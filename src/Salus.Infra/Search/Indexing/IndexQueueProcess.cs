namespace Salus.Infra.Search.Indexing
{
    using DataAccess;
    using Logs;
    using Model.Repositorios;
    using Model.Search;
    using Model.Servicos;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Model.Entidades;
    using System.Collections;
    public class IndexQueueProcess : IIndexQueueProcess
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IConfiguracoesDaAplicacao configuracoesDaAplicacao;
        private readonly LuceneIndexerSession indexerSession;
        private readonly IndexQueueProcessBatch indexQueueProcessBatch;
        private readonly IUnidadeDeTrabalho unidadeDeTrabalho;
        private int contentsIndexed;

        public IndexQueueProcess(
            IDocumentoRepositorio documentoRepositorio,
            IConfiguracoesDaAplicacao configuracoesDaAplicacao,
            IUnidadeDeTrabalho unidadeDeTrabalho,
            LuceneIndexerSession indexerSession,
            IndexQueueProcessBatch indexQueueProcessBatch)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.configuracoesDaAplicacao = configuracoesDaAplicacao;
            this.unidadeDeTrabalho = unidadeDeTrabalho;
            this.indexerSession = indexerSession;
            this.indexQueueProcessBatch = indexQueueProcessBatch;
        }

        public void Execute()
        {
            dynamic ids = default(dynamic);

            DapperHelper.UsingConnection(conn =>
            {
                var sql =
@"select 
    id doc
from 
    documento 
where
    search_status = 1";

                ids = conn.Query(sql);
            });

            var documentosIds = ((IEnumerable<dynamic>)ids).AsList<dynamic>();

            if (documentosIds.Count == 0)
            {
                Log.App.InfoFormat("Não há documentos pendentes para indexação");
                return;
            }

            var count = 0;

            using (var session = this.indexerSession
                    .Begin(this.configuracoesDaAplicacao.CaminhoIndicePesquisa()))
            {
                Parallel.For(0, documentosIds.Count, new ParallelOptions { MaxDegreeOfParallelism = 16 }, i =>
                 {
                     if (count % 5000 == 0)
                     {
                         Log.App.Info("Otimizando indices..." + count);
                         session.Current.Commit();
                     }

                     this.Increment(this.indexQueueProcessBatch.Execute(documentosIds[i].doc));

                     Interlocked.Increment(ref count);
                 });
            }

            using (var session = this.indexerSession
                    .Begin(this.configuracoesDaAplicacao.CaminhoIndicePesquisa()))
            {
                session.Current.Optimize();
            }

            Log.App.Info("documentos indexados com sucesso");
        }

        private void Increment(int indexed)
        {
            Interlocked.Add(ref this.contentsIndexed, indexed);
        }
    }
}
