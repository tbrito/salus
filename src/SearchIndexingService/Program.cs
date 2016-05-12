using Quartz;
using Quartz.Impl;
using Salus.Infra;
using Salus.Infra.DataAccess;
using Salus.Infra.Logs;
using SharpArch.NHibernate;
using System;
using System.Collections.Specialized;
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

            NHibernateSession.CloseAllSessions();
            UnidadeDeTrabalho.Boot();

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
            var propriedade = new NameValueCollection
            {
                { "quartz.threadPool.threadCount", "1" }
            };

            scheduler = new StdSchedulerFactory(propriedade).GetScheduler();
            scheduler.JobFactory = new StructureMapJobFactory();

            scheduler.Start();

            IJobDetail job = JobBuilder.Create<IndexarJob>()
                .WithIdentity("indexarJob", "indexarGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("triggerIndexarJob", "indexarGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
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
