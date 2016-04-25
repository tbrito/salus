using Quartz;
using Salus.Infra.Logs;
using Salus.Model.Search;
using SharpArch.NHibernate;

namespace SearchIndexingService
{
    public class IndexarJob : IJob
    {
        private IIndexQueueProcess indexQueueProcess;

        public void Execute(IJobExecutionContext context)
        {
            using (var transaction = NHibernateSession.Current.Transaction)
            {
                try
                {
                    this.indexQueueProcess.Execute();
                    transaction.Commit();
                }
                catch (System.Exception exception)
                {
                    transaction.Rollback();
                    Log.App.Error("Erro ao tentar indexar documentos. ", exception);
                }
            }
        }
    }
}
