using System.ServiceProcess;

namespace SearchIndexingService
{
    public class ServiceRunner : ServiceBase
    {
        public ServiceRunner()
        {
            ServiceName = ServiceName;
        }

        protected override void OnStart(string[] args)
        {
            Program.Start(args);
        }

        protected override void OnStop()
        {
            Program.Stop();
        }
    }
}
