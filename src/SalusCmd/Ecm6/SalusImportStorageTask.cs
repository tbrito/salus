namespace SalusCmd.Tarefas.Ecm6
{
    using Limilabs.FTP.Client;
    using Salus.Infra.Extensions;
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Infra.Repositorios;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Entidades.Import;
    using Salus.Model.Repositorios;
    using SalusCmd.Ecm6;
    using SharpArch.NHibernate;
    using System;
    using System.Configuration;
    using System.IO;

    public class SalusImportStorageTask : ITarefa
    {
        private readonly IMongoStorage mongoStorage = InversionControl.Current.Resolve<IMongoStorage>();
        private readonly IStorageRepositorio storageRepositorio = InversionControl.Current.Resolve<IStorageRepositorio>();
        private int filesMoved;
        private Ftp ftpClient;

        public string TextoDeAjuda
        {
            get
            {
                return "Importa storage do ged6";
            }
        }

        public string Comando
        {
            get
            {
                return "import storage";
            }
        }

        public void Executar(params string[] args)
        {
            var now = DateTime.Now;
            var tempPath = this.GetImportTempPath();

            Directory.CreateDirectory(tempPath);

            Log.App.InfoFormat("Convertendo storage do ged 6 para o salus");
            Log.App.InfoFormat("Pasta temporária: " + tempPath);
            Log.App.InfoFormat("Aguarde a execução até o fim. Não interrompa.");

            this.ftpClient = this.BuildFtpClient();

            var rootpath = this.ftpClient.GetCurrentFolder();

            Log.App.InfoFormat(
                "Caminho do repositório antigo: {0}",
                rootpath);

            this.ProcessDirectory(rootpath);

            Log.App.InfoFormat(
                "Repositório convertido em {0}. {1} arquivos importados",
                DateTime.Now.Subtract(now),
                this.filesMoved);
        }

        private Ftp BuildFtpClient()
        {
            Ftp ftp = null;

            ImportDatabase.Using(session =>
            {
                const string Sql = @"
select 
	system_serverip Host,
	system_port Port,
	SYSTEM_SERVERUSR FtpUser,
	SYSTEM_SERVERPWD Password,
	SYSTEM_FTPROOT RootPath
from system";

                var ftpSettings = session.CreateSQLQuery(Sql)
                    .SetResultTransformer(CustomResultTransformer<FtpSettings>.Do())
                    .UniqueResult<FtpSettings>();

                ftp = new Ftp();

                ftp.Connect(ftpSettings.Host, ftpSettings.Port);
                ftp.Login(ftpSettings.FtpUser, ftpSettings.Password);

                ////ftp.Connect("192.168.10.69", 21);
                ////ftp.Login("anonymous", "anonymous");
            });

            return ftp;
        }

        private void ProcessDirectory(string directoryBase)
        {
            Log.App.InfoFormat("Processando diretório " + directoryBase);

            if (directoryBase.ToLower() == "/rpt")
            {
                this.ProcessRptDirectory(directoryBase);
            }
            else if (directoryBase.ToLower() == "/revisao")
            {
                this.ProcessVersioningDirectory(directoryBase);
            }
            else
            {
                this.ProcessDocumentDirectory(directoryBase);
            }

            Log.App.InfoFormat("Diretório " + directoryBase + " processado");
        }

        /// <summary>
        /// Processa diretório de documentos
        /// </summary>
        /// <param name="directoryBase">Diretório base</param>
        private void ProcessDocumentDirectory(string directoryBase)
        {
            foreach (var directory in this.ftpClient.GetList(directoryBase))
            {
                if (directory.IsFolder)
                {
                    this.ProcessDirectory(directory.Name);
                }
                ////this.ProcessDirectory(directoryBase + directory + "/");
            }

            Log.App.InfoFormat("Diretório é de documentos");

            foreach (var file in this.ftpClient.GetList(directoryBase))
            {
                if (Path.GetFileNameWithoutExtension(file.Name).IsInt())
                {
                    this.ImportEcm6DocumentFile(directoryBase, file.Name);
                }
            }
        }

        private void ImportEcm6DocumentFile(string directoryBase, string file)
        {
            var docId = Path.GetFileNameWithoutExtension(file).ToInt();

            var documento = this.storageRepositorio
                .ObterPorSalusId("[documento]" + docId.ToString());

            if (documento != null)
            {
                Log.App.Info("{0} já foi migrado");
                return;
            }

            this.SendFileToContent(directoryBase, file, docId);
        }

        /// <summary>
        /// Processa diretório de versionamento
        /// </summary>
        /// <param name="directoryBase">Diretório base</param>
        private void ProcessVersioningDirectory(string directoryBase)
        {
            Log.App.InfoFormat("Diretório é de versionamento");

            foreach (var directory in Directory.GetDirectories(directoryBase))
            {
                this.ProcessVersioningDirectory(directory);
            }

            foreach (var file in Directory.GetFiles(directoryBase))
            {
                if (Path.GetFileNameWithoutExtension(file).IsInt())
                {
                    this.SendRevisionDocument(file);
                }
            }
        }

        /// <summary>
        /// Processa diretório de modelos
        /// </summary>
        /// <param name="directoryBase">Diretório base</param>
        private void ProcessRptDirectory(string directoryBase)
        {
            Log.App.InfoFormat("Diretório é de modelos");

            foreach (var file in Directory.GetFiles(directoryBase))
            {
                this.SendFileToContent(Path.GetFileName(file), file, 0);
            }
        }

        /// <summary>
        /// Envia documento versionado para o content
        /// </summary>
        /// <param name="file">Arquivo a ser enviado</param>
        private void SendRevisionDocument(string file)
        {
            var vsdoc = new VsDoc();
            var documentKey = Path.GetFileNameWithoutExtension(file).ToInt();

            ImportDatabase.Using(session =>
            {
                var sql = "select doc_code DocCode, vsdoc_revisao VsDocRevisao from vsdoc where vsdoc_code = " +
                    documentKey;

                vsdoc = session.CreateSQLQuery(sql)
                    .SetResultTransformer(CustomResultTransformer<VsDoc>.Do())
                    .UniqueResult<VsDoc>();
            });

            var ecm6DocVersionado = this.GetEcm6VersionedDocument(documentKey);

            if (ecm6DocVersionado == null)
            {
                Log.App.ErrorFormat("Documento ecm6  #{0} não foi importado", vsdoc.DocCode);
                return;
            }

            this.ImportEcm6VersionedDocumentFile(ecm6DocVersionado, file, vsdoc);
        }

        private void ImportEcm6VersionedDocumentFile(Ecm6DocVersionado ecm6DocVersionado, string file, VsDoc vsdoc)
        {
            try
            {
                var docId = vsdoc.DocCode.ToInt();

                if (ecm6DocVersionado.ImportStatus == Ecm6ImportStatus.NotImported)
                {
                    this.SendFileToContent(
                        string.Format(
                       "{0}.{1}",
                       vsdoc.DocCode,
                       Convert.ToInt32(vsdoc.VsDocRevisao.Substring(3))), file, docId);

                    ecm6DocVersionado.ImportStatus = Ecm6ImportStatus.Imported;
                    this.UpdateImportStatus(ecm6DocVersionado);
                }
            }
            catch (Exception exception)
            {
                Log.App.Error("Erro ao enviar arquivo para o repositório", exception);
                ecm6DocVersionado.ImportStatus = Ecm6ImportStatus.NotImported;
                this.UpdateImportStatus(ecm6DocVersionado);
            }
        }

        private void SendFileToContent(string directory, string file, int ecm8Id)
        {
            Log.App.InfoFormat("Movendo " + file);
            var downloadedFromFtp = Path.Combine(this.GetImportTempPath(), Path.GetFileName(file));

            this.ftpClient.Download(downloadedFromFtp, file);

            try
            {
                this.mongoStorage.AdicionarOuAtualizar(file);
                this.filesMoved++;
            }
            catch (Exception ex)
            {
                Log.App.ErrorFormat("Erro ao importar {0}", downloadedFromFtp, ex);
            }

            try
            {
                File.Delete(downloadedFromFtp);
            }
            catch (Exception ex)
            {
                Log.App.ErrorFormat("Erro ao apagar {0}", file, ex);
            }

            Log.App.InfoFormat("Arquivo {0} migrado", file);
        }

        private string GetImportTempPath()
        {
            var path = ConfigurationManager.AppSettings["Import.Temp"];

            if (string.IsNullOrEmpty(path))
            {
                return Path.GetTempPath();
            }

            return path;
        }

        private void UpdateImportStatus<T>(T entidade) where T : Entidade
        {
            NHibernateSession.Current.SaveOrUpdate(entidade);
        }

        private Ecm6DocVersionado GetEcm6VersionedDocument(int ecm6DocVersion)
        {
            Ecm6DocVersionado ecm6Docs;

            ecm6Docs = NHibernateSession
                .Current
                .CreateSQLQuery("select id Id, ecm6_id Ecm6Id, ecm8_id Ecm8Id, import_status ImportStatus from ecm6_docversionado where ecm6_id = :ecm6Id")
                .SetInt32("ecm6Id", ecm6DocVersion)
                .SetResultTransformer(CustomResultTransformer<Ecm6DocVersionado>.Do())
                .UniqueResult<Ecm6DocVersionado>();

            return ecm6Docs;
        }

        internal class FtpSettings
        {
            public string Host
            {
                get;
                set;
            }

            public string FtpUser
            {
                get;
                set;
            }

            public string Password
            {
                get;
                set;
            }

            public string RootPath
            {
                get;
                set;
            }

            public int Port
            {
                get;
                set;
            }
        }

        internal class VsDoc
        {
            public string DocCode
            {
                get;
                set;
            }

            public string VsDocRevisao
            {
                get;
                set;
            }
        }
    }
}
