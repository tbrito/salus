namespace Salus.IntegrationTests
{
    using Infra.ConnectionInfra;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Salus.Infra.Migrations;
    using SharpArch.NHibernate;
    using System;
    using System.Configuration;
    using System.IO;

    [TestClass]
    public abstract class TesteAutomatizado
    {
        [AssemblyInitialize()]
        public void TestInit()
        {
            var migrator = new Migrator(
                ConfigurationManager.AppSettings["Database.ConnectionString"]);

            migrator.Migrate(runner => runner.MigrateUp());

            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
            };

            NHibernateSession.Init(
                new SimpleSessionStorage(),
                mappings,
                null, null, null, null, BancoDeDados.Configuration());

            NHibernateSession.Current.FlushMode = NHibernate.FlushMode.Commit;
        }
    }
}
