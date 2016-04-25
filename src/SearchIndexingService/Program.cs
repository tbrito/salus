using System;
using System.ServiceProcess;

namespace SearchIndexingService
{
    public class Program
    {
        public const string ServiceName = "SalusSearchIndex";

        public static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                using (var service = new ServiceRunner())
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                Start(args);
            }
        }

        public static void Start(string[] args)
        {
            Console.Write("Oi");
            Console.ReadKey();
        }

        public static void Stop()
        {
            // onstop code here
        }
    }
}
