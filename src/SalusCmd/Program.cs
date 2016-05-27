using FluentNHibernate.Cfg;
using Salus.Infra;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.DataAccess;
using Salus.Infra.IoC;
using Salus.Infra.Logs;
using Salus.Infra.Util;
using SalusCmd.Comandos;
using SalusCmd.Ecm6;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalusCmd
{
    public class Program
    {
        private static Dictionary<string, Func<ITarefa>> comandos = new Dictionary<string, Func<ITarefa>>();

        public static void Main(string[] args)
        {
            Aplicacao.Boot();

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

            comandos.Add(
               "schema update",
               () => InversionControl.Current.Resolve<SchemaUpdateTask>());

            comandos.Add(
               "import data",
               () => InversionControl.Current.Resolve<SalusImportDatabaseTask>());

            comandos.Add(
                "import storage",
                () => InversionControl.Current.Resolve<SalusImportStorageTask>());
            
            ExecutarTarefa(args);
        }

        private static void ExecutarTarefa(string[] args)
        {
            var argumentos = string.Join(" ", args);

            if (comandos.ContainsKey(argumentos.Trim()))
            {
                comandos[argumentos.Trim()]().Executar();
            }
            else
            {
                Log.App.ErrorFormat("Não foi encontrado tarefa para {0}", argumentos);
            }
        }
    }
}
