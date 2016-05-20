using FluentNHibernate.Cfg;
using Salus.Infra;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.DataAccess;
using Salus.Infra.IoC;
using Salus.Infra.Logs;
using Salus.Infra.Migrations;
using Salus.Infra.Util;
using SalusCmd.Ecm6;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SalusCmd
{
    public class Program
    {
        private static Dictionary<string, Func<ITarefa>> comandos = new Dictionary<string, Func<ITarefa>>();

        public static void Main(string[] args)
        {
            Aplicacao.Boot();

            ////comandos.Add(
            ////   "key generate",
            ////   () => InversionControl.Current.Resolve<GenerateKeyTask>());

            comandos.Add(
               "import data",
               () => InversionControl.Current.Resolve<Ecm6ImportDatabaseTask>());

            comandos.Add(
                "import storage",
                () => InversionControl.Current.Resolve<Ecm6ImportStorageTask>());

            InicializaBancoDeDados();

            ExecutarTarefa(args);

        }

        private static void ExecutarTarefa(string[] args)
        {
            var argumentos = args.ToString();

            if (comandos.ContainsKey(argumentos))
            {
                comandos[argumentos]().Executar();
            }
            else
            {
                Log.App.ErrorFormat("Não foi encontrado tarefa para {0}", argumentos);
            }
        }

        private static void InicializaBancoDeDados()
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
            };

            NHibernateInitializer.Instance().InitializeNHibernateOnce(() =>
            {
                NHibernateSession.Init(
                    new ThreadSessionStorage(),
                    mappings,
                    null, null, null, null, BancoDeDados.Configuration());
            });

            var fluentConfiguration = Fluently.Configure()
               .Database(BancoDeDados.Configuration())
               .Mappings(m =>
               {
                   m.FluentMappings.Conventions.Add<EnumConvention>();
                   m.FluentMappings.Conventions.Add<EnumerationTypeConvention>();
               });


            var migrator = new Migrator(
              ConfigurationManager.AppSettings["Database.ConnectionString"]);

            migrator.Migrate(runner => runner.MigrateUp());
        }
    }
}
