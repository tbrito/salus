using Salus.Infra;
using Salus.Infra.IoC;
using Salus.Infra.Logs;
using SalusCmd.Ecm6;
using System;
using System.Collections.Generic;

namespace SalusCmd
{
    public class Program
    {
        private static Dictionary<string, Func<ITarefa>> comandos = new Dictionary<string, Func<ITarefa>>();

        public static void Main(string[] args)
        {
            Aplicacao.Boot();

            comandos.Add(
               "schema update",
               () => InversionControl.Current.Resolve<SchemaUpdateTask>());

            comandos.Add(
               "import data",
               () => InversionControl.Current.Resolve<Ecm6ImportDatabaseTask>());

            comandos.Add(
                "import storage",
                () => InversionControl.Current.Resolve<Ecm6ImportStorageTask>());

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
    }
}
