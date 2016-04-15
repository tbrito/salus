namespace Salus.IntegrationTests.Boot
{
    using Infra.Repositorios;
    using Salus.Infra.ConnectionInfra;
    using Salus.Infra.Migrations;
    using SharpArch.NHibernate;
    using System;
    using System.Configuration;
    using System.IO;

    public class DatabaseBoot : IDatabaseBoot
    {
        public void Execute()
        {
            var migrator = new Migrator(
                ConfigurationManager.AppSettings["Database.ConnectionString"]);

            migrator.Migrate(runner => runner.MigrateUp());
        }
    }
}
