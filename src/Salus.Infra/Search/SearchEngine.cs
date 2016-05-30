namespace Salus.Infra.Search
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Lucene.Net.Analysis;
    using Lucene.Net.Documents;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Model.Servicos;
    using Logs;
    using Lucene.Net.Search.Highlight;
    using Extensions;
    using Model.Search;

    public class SearchEngine : LuceneEngineBase, ISearchEngine
    {
        public SearchEngine(
            IConfiguracoesDaAplicacao configuracoesDaAplicacao) :
            base(configuracoesDaAplicacao)
        {
        }

        public TokenStream Stream
        {
            get;
            private set;
        }

        public Highlighter Highlighter
        {
            get;
            private set;
        }

        public IList<int> SearchDocumentosIds(string text, int tipodocumentoId = 0, string startDate = "", string endDate = "")
        {
            Log.App.InfoFormat("Pesquisando conteudos com o texto: {0}", text);
            var textQuery = new TextQuery(text);
            return this.Search(textQuery.Incremented, tipodocumentoId, startDate, endDate);
        }

        private IList<int> Search(string text, int tipodocumentoId, string startDate, string endDate)
        {
            var directory = this.GetDirectory();
            var indexReader = this.GetIndexReader(directory);
            var searcher = new IndexSearcher(indexReader);

            try
            {
                var query = this.BuildQuery(text, tipodocumentoId);
                var filter = this.BuildDateFilter(startDate, endDate);

                var sort = new Sort(new SortField("dataCriacao", SortField.LONG, true));

                var docs = searcher.Search(query, filter, this.configuracoesDaAplicacao.ResultadoMaximoConsulta, sort);

                // create highlighter
                var formatter = new SimpleHTMLFormatter("<span class=\"result-highlight\">", "</span>");
                var scorer = new QueryScorer(query);
                this.Highlighter = new Highlighter(formatter, scorer);
                this.Stream = LuceneEngineBase.GetAnalyzer().TokenStream(string.Empty, new StringReader(text));

                return this.BuildSearchResult(docs, searcher);
            }
            finally
            {
                searcher.Dispose();
                indexReader.Dispose();
                directory.Dispose();
            }
        }

        private Query BuildQuery(string text, int tipodocumentoId)
        {
            var queryText = "(assunto:* OR indexacao:*)";

            
            if (string.IsNullOrEmpty(text) == false)
            {
                queryText = string.Format(
                    "(assunto:{0} OR indexacao:{0})", 
                    text);
            }

            if (text.IsInt())
            {
                queryText = string.Format(
                    "(documentoId:{0})",
                    text);
            }

            if (tipodocumentoId > 0)
            {
                queryText = string.Format("tipoDocumentoId:{0} AND ", tipodocumentoId) + queryText;
            }

            var query = this.GetParsedQuery(queryText);

            Log.App.DebugFormat("Consulta: {0}", query);

            return query;
        }

        private Query GetParsedQuery(string queryText)
        {
            var queryParser = new QueryParser(
                LuceneEngineBase.GetVersion(), "assunto", LuceneEngineBase.GetAnalyzer())
            {
                AllowLeadingWildcard = true
            };
            
            return queryParser.Parse(queryText);
        }

        private IList<int> BuildSearchResult(TopDocs topDocs, Searchable indexSearcher)
        {
            var total = topDocs.TotalHits;
            var result = new List<int>();

            for (int i = 0; i < total; i++)
            {
                if (i == this.configuracoesDaAplicacao.ResultadoMaximoConsulta)
                {
                    break;
                }

                var docIndex = topDocs.ScoreDocs[i].Doc;
                var doc = indexSearcher.Doc(docIndex);
                result.Add(doc.Get("documentoId").ToInt());
            }
            
            return result;
        }
        
        private FieldCacheRangeFilter<string> BuildDateFilter(string startDate, string endDate)
        {
            var includeLower = false;
            var includeUpper = false;
            string start = null;
            string end = null;

            if (string.IsNullOrEmpty(startDate) == false)
            {
                includeLower = true;

                start = DateTools.DateToString(
                    Convert.ToDateTime(startDate), 
                    DateTools.Resolution.DAY);
            }

            if (string.IsNullOrEmpty(endDate) == false)
            {
                includeUpper = true;

                end = DateTools.DateToString(
                    Convert.ToDateTime(endDate), 
                    DateTools.Resolution.DAY);
            }

            return FieldCacheRangeFilter.NewStringRange(
                "dataCriacao", 
                start, 
                includeLower: includeLower, 
                upperVal: end, 
                includeUpper: includeUpper);
        }
    }
}