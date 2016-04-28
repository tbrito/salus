using Quartz;
using Quartz.Impl;
using Salus.Infra;
using Salus.Infra.Logs;
using System;
using System.ServiceProcess;

namespace SearchIndexingService
{
    public class Program
    {
        public const string ServiceName = "SalusSearchIndex";
        private static IScheduler scheduler;

        public static void Main(string[] args)
        {
            Aplicacao.Boot();
            Log.Initialize();
            Log.App.Info("Aplicacao Iniciada");

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
            scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            IJobDetail job = JobBuilder.Create<IndexarJob>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever()
                    .WithMisfireHandlingInstructionIgnoreMisfires())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public static void Stop()
        {
            scheduler.Shutdown();
        }
    }
}
