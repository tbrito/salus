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
            var log2File = Path.Combine(Aplicacao.Caminho, @"Logs\Web2.log");
            File.Copy(logFile, log2File);

            var texto = File.ReadAllText(log2File);

            var log = new LogViewModel();
            log.Texto = texto;

            return log;
        }
    }
}