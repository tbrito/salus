﻿namespace Salus.Infra.Search.Indexing
{
    using DataAccess;
    using Logs;
    using Model.Entidades;
    using Model.Repositorios;
    using Model.Search;
    using Model.Servicos;
    using SharpArch.NHibernate;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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
            IList<Documento> contents;

            do
            {
                using (this.unidadeDeTrabalho.Iniciar())
                {
                    contents = this.documentoRepositorio.ObterTodosParaIndexar(80);
                }

                if (contents.Count == 0)
                {
                    Log.App.InfoFormat("Não há documentos pendentes para indexação");
                    return;
                }

                Log.App.InfoFormat(
                    "Indexando documentos de {0} a {1}",
                    contents[0].Id,
                    contents.Last().Id);

                using (var session = this.indexerSession
                    .Begin(this.configuracoesDaAplicacao.CaminhoIndicePesquisa()))
                {
                    Parallel.ForEach(contents, new ParallelOptions() { MaxDegreeOfParallelism = Aplicacao.Nucleos }, batch =>
                    {
                        using (var transacao = this.unidadeDeTrabalho.Iniciar())
                        {
                            try
                            {
                                this.Increment(this.indexQueueProcessBatch.Execute(batch));
                                transacao.Commit();
                            }
                            catch (System.Exception exception)
                            {
                                transacao.RollBack();
                                Log.App.Error("Erro ao tentar indexar documentos. ", exception);
                            }
                        }
                    });
                }
            } while (contents.Count > 0);
        }

        private void Increment(int indexed)
        {
            Interlocked.Add(ref this.contentsIndexed, indexed);
        }
    }
}