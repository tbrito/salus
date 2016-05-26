using Salus.Infra;
using Salus.Infra.IoC;
using Salus.Infra.Logs;
using SalusCmd.Comandos;
using SalusCmd.Ecm6;
using System;
using System.Collections.Generic;
using System.Linq;

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
