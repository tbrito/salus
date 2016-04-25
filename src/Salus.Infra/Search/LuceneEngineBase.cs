namespace Salus.Infra.Search
{
    using System.IO;
    using Analyzers;
    using Lucene.Net.Analysis;
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Logs;
    using Model.Servicos;

    public abstract class LuceneEngineBase
    {
        protected readonly IConfiguracoesDaAplicacao configuracoesDaAplicacao;

        protected LuceneEngineBase(IConfiguracoesDaAplicacao configuracoesDaAplicacao)
        {
            this.configuracoesDaAplicacao = configuracoesDaAplicacao;
        }

        public static Analyzer GetAnalyzer()
        {
            return new BrazilianAnalyzerCustom(LuceneEngineBase.GetVersion());
        }

        public static Lucene.Net.Util.Version GetVersion()
        {
            return Lucene.Net.Util.Version.LUCENE_30;
        }

        protected FSDirectory GetDirectory()
        { 
            var indexPath = this.configuracoesDaAplicacao.CaminhoIndicePesquisa();
            Log.App.DebugFormat("Abrindo indice em {0}", indexPath);

            return FSDirectory.Open(new DirectoryInfo(indexPath));
        }

        protected IndexReader GetIndexReader(FSDirectory directory)
        {
            return this.GetIndexReader(directory, true);
        }

        protected IndexReader GetIndexReader(FSDirectory directory, bool readOnly)
        {
            return IndexReader.Open(directory, readOnly);
        }

        protected IndexWriter GetIndexWriter(FSDirectory directory)
        {
            return new IndexWriter(
                this.GetDirectory(),
                LuceneEngineBase.GetAnalyzer(),
                IndexWriter.MaxFieldLength.UNLIMITED);
        }
    }
}
