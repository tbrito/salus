using Salus.Infra;
using Salus.Infra.Logs;

namespace Salus.IntegrationTests
{
    public class TesteAutomatizado
    {
        static TesteAutomatizado()
        {
            Aplicacao.Boot();

            Log.App.Debug("Iniciando teste automatizado");
        }
    }
}