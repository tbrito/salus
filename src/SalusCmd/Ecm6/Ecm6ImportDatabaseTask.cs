namespace SalusCmd.Ecm6
{
    using Salus.Infra.Logs;
    using SalusCmd;
    using SalusCmd.Ecm6.Imports;

    public class Ecm6ImportDatabaseTask : ITarefa
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Importa dados do Ecm6";
            }
        }

        public string Comando
        {
            get
            {
                return "ecm6 import database";
            }
        }

        public void Executar(params string[] args)
        {
            InternalExecute();
        }

        private static void InternalExecute()
        {
            Log.App.InfoFormat("Importação iniciada");

            new BulkTipoDocumentoImport().Execute("Importando tipodocumento");

            new BulkChaveImport().Execute("Importando chaves");

            new BulkPerfilImport().Execute("Importando perfis");
            new BulkAreaImport().Execute("Importando areas");

            new BulkUsuarioImport().Execute("Importando usuarios");

            new PerfilTipoDocumentoImport().Execute("Importando perfil x tipodocumento");

            new BulkDocumentoImport().Execute("Importando documentos");
            new BulkDocumentoIndiceImport().Execute("Importando indices");

            new BulkVersaoDocumentoImport().Execute("Importando versao do documento");

            new BulkIndexImport().Execute("Importando indexacao");
            new BulkWorkflowImport().Execute("Importando workflow");

            var documentosPreindexados = new DocumentoPreindexadoImport().Execute("Importando preindexados");
            new PreIndexIndexesImport(documentosPreindexados).Execute("Importando chaves de preindex");
            
            Log.App.Info("Importação finalizada");
        }
    }
}