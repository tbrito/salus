namespace Veros.Ecm.DataAccess.Tarefas.Ecm6
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
            
            var systemKeys = new SystemKeysImport().Execute("Importando system keys");
            new BulkKeyImport(systemKeys).Execute("Importando keys");

            ////new BulkProfileImport().Execute("Importando profiles");
            ////new BulkAreaImport().Execute("Importando areas");

            ////new BulkUserImport().Execute("Importando users");
            ////new BulkGroupMemberImport().Execute("Importando group members");
            ////new ProfileCategoriesImport().Execute("Importando profile x category");

            ////new BulkFileImport().Execute("Importando docs");
            ////new BulkDossierImport().Execute("Importando dossiers");

            ////new BulkFileVersionImport().Execute("Importando docs versions");

            ////new BulkIndexImport().Execute("Importando indexes");
            ////new BulkShareImport().Execute("Importando shares");

            ////var preIndexes = new PreIndexFileImport().Execute("Importando preindex");
            ////new PreIndexIndexesImport(preIndexes).Execute("Importando chaves de preindex");

            ////new BulkPurgesImport().Execute("Importando dados expurgo");

            Log.App.Info("Importação finalizada");
        }
    }
}