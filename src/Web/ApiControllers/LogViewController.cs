namespace Web.ApiControllers
{
    using Salus.Infra;
    using Salus.Model;
    using System.IO;
    using System.Web.Http;

    public class LogViewController : ApiController
    {
        [HttpGet]
        public LogViewModel Obter(int id = 0)
        {
            var logFile = Path.Combine(Aplicacao.Caminho, @"Logs\Web.log");
            var texto = File.ReadLines(logFile);

            var log = new LogViewModel();
            log.Texto = texto;

            return log;
        }
    }
}