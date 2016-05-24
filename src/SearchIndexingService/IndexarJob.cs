using Quartz;
using Salus.Infra.IoC;
using Salus.Model.Search;

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
            this.indexQueueProcess.Execute();
        }
    }
}
