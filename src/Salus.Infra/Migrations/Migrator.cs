using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;
using System;
using System.Reflection;

namespace Salus.Infra.Migrations
{
    public class Migrator
    {
        private string connectionString;

        public Migrator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly
            {
                get;
                set;
            }

            public string ProviderSwitches
            {
                get;
                set;
            }

            public int Timeout
            {
                get;
                set;
            }
        }

        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            var options = new MigrationOptions
            {
                PreviewOnly = false,
                Timeout = 0
            };

            var factory = new SqlServer2008ProcessorFactory();
            var announer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var migrationContext = new RunnerContext(announer);
            var processor = factory.Create(this.connectionString, announer, options);
            var runner = new MigrationRunner(Assembly.GetExecutingAssembly(), migrationContext, processor);

            runnerAction(runner);
        }
    }
}
