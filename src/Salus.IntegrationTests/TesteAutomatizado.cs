namespace Salus.IntegrationTests
{
    using Infra.ConnectionInfra;
    using NUnit.Framework;
    using Salus.Infra.Migrations;
    using SharpArch.NHibernate;
    using System;
    using System.Configuration;
    using System.IO;

    [SetUpFixture]
    public abstract class TesteAutomatizado
    {
        [OneTimeSetUp]
        public void TestInit()
        {
            var migrator = new Migrator(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

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
