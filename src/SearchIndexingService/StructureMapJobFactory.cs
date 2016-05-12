using System;
using Quartz;
using Quartz.Spi;
using Salus.Infra.IoC;

namespace SearchIndexingService
{
    public class StructureMapJobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)InversionControl.Current.Resolve(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                var se = new SchedulerException("falha ao agendar", e);
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}