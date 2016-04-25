namespace Salus.Infra.Search
{
    using System;
    using System.IO;
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Logs;

    public class LuceneIndexerSession
    {
        private static FSDirectory directory;
        private static IndexWriter writer;

        public IndexWriter Current
        {
            get
            {
                if (writer == null)
                {
                    throw new InvalidOperationException(
                        "Sessao do lucene nao foi iniciado. Voce deve chamar o metodo Begin");
                }

                return writer;
            }
        }

        public void Dispose()
        {
            Log.App.DebugFormat("Fechando indice");

            writer.Dispose();
            directory.Dispose();

            writer = null;
            directory = null;
        }

        public LuceneIndexerSession Begin(string indexDirectory)
        {
            Log.App.DebugFormat("Abrindo indice em {0}", indexDirectory);

            directory = FSDirectory.Open(new DirectoryInfo(indexDirectory));

            writer = new IndexWriter(
                directory,
                LuceneEngineBase.GetAnalyzer(),
                IndexWriter.MaxFieldLength.UNLIMITED);

            return this;
        }
    }
}