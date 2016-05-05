using Quartz;
using Salus.Infra.DataAccess;
using Salus.Infra.IoC;
using Salus.Model.Search;
using SharpArch.NHibernate;

namespace SearchIndexingService
{
    [DisallowConcurrentExecution]
    public class IndexarJob : IJob
    {
        private IIndexQueueProcess indexQueueProcess;

        public IndexarJob()
        {
            this.indexQueueProcess = InversionControl.Current.Resolve<IIndexQueueProcess>();
        }

        public void Execute(IJobExecutionContext context)
        {
            NHibernateSession.CloseAllSessions();
            UnidadeDeTrabalho.Boot();
            this.indexQueueProcess.Execute();
        }
    }
}
