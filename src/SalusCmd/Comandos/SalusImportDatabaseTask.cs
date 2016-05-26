namespace SalusCmd.Comandos
{
    using Ecm6.Imports;
    using Salus.Infra.Logs;
    using SalusCmd;

    public class SalusImportDatabaseTask : ITarefa
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Importa dados do ged6";
            }
        }

        public string Comando
        {
            get
            {
                return "salus import database";
            }
        }

        public void Executar(params string[] args)
        {
            InternalExecute();
        }

        private static void InternalExecute()
        {
            Log.App.InfoFormat("Importação iniciada");

            new BulkTipoDocumentoImport().Execute("Importando Tipo de Documento");

            new BulkChaveImport().Execute("Importando Chaves dos documentos");

            new BulkPerfilImport().Execute("Importando Perfis");
            new BulkAreaImport().Execute("Importando Areas");

            new BulkUsuarioImport().Execute("Importando Usuarios");

            new PerfilTipoDocumentoImport().Execute("Importando Perfil -> Tipo de Documento");

            new BulkDocumentoImport().Execute("Importando Documentos");
            new BulkDocumentoIndiceImport().Execute("Importando Indices");

            new BulkVersaoDocumentoImport().Execute("Importando Versoes de documento");

            new BulkIndexImport().Execute("Importando Indexacao de Documento");
            new BulkWorkflowImport().Execute("Importando Workflow");

            var documentosPreindexados = new DocumentoPreindexadoImport().Execute("Importando Preindexados");
            new PreIndexIndexesImport(documentosPreindexados).Execute("Importando Chaves de Preindexados");
            
            Log.App.Info("Importação finalizada");
        }
    }
}