namespace Salus.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Salus.Infra.Migrations;
    using System.Configuration;

    [TestClass]
    public class AssemblyInitializeTest
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext contexto)
        {
            var migrator = new Migrator(
                ConfigurationManager.AppSettings["Database.ConnectionString"]);
            
            migrator.Migrate(runner => runner.MigrateUp());
        }
    }
}
