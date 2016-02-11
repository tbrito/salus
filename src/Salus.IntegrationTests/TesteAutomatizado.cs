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
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
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
        }
    }
}
