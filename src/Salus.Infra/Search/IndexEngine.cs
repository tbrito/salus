namespace Salus.Infra.Search
{
    using System;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Salus.Model.Servicos;
    using Salus.Model.Entidades;
    using System.Collections.Generic;
    using Salus.Infra.Logs;
    using Model.Search;
    using System.Linq;
    using Model;
    public class IndexEngine : LuceneEngineBase, IIndexEngine
    {
        private readonly LuceneIndexerSession luceneSession;

        public IndexEngine(
            IConfiguracoesDaAplicacao configuracoesDaAplicacao,
            LuceneIndexerSession luceneSession) : base(configuracoesDaAplicacao)
        {
            this.luceneSession = luceneSession;
        }

        public SearchStatus Index(Documento Documento, IList<Indexacao> indexacao)
        {
            try
            {
                this.DeleteContentIfExist(Documento.Id);
                this.AddDocument(Documento, indexacao);
            }
            catch (Exception exception)
            {
                Log.App.Error("Erro ao indexar conteudo #" + Documento.Id, exception);

                return SearchStatus.TryIndexAgain;
            }
            
            return SearchStatus.Indexed;
        }

        public void DeleteContentIfExist(int contentId)
        {
            this.luceneSession.Current.DeleteDocuments(new Term("contentId", contentId.ToString()));
        }

        private void AddDocument(Documento documento, IList<Indexacao> indexacao)
        {
            var luceneDocument = new Document();

            luceneDocument.Add(this.GetContentIdField(documento));
            luceneDocument.Add(this.GetIndexacaoField(indexacao));
            luceneDocument.Add(this.GetCpfCnpj(documento));

            if (documento.TipoDocumento != null)
            {
                luceneDocument.Add(this.GetTipoDocumento(documento));
            }

            if (string.IsNullOrEmpty(documento.Assunto) == false)
            {
                luceneDocument.Add(this.GetAssuntoDocumento(documento));
            }

            if (documento.DataCriacao != null)
            {
                luceneDocument.Add(this.GetDataCriacao(documento));
            }

            this.luceneSession.Current.AddDocument(luceneDocument);
        }

        private Field GetCpfCnpj(Documento content)
        {
            string valorCpf = content.CpfCnpj;

            if (string.IsNullOrEmpty(valorCpf))
            {
                var cpf = content.Indexacao.FirstOrDefault(x => x.Chave.TipoDado == TipoDado.CpfCnpj);

                if (cpf == null)
                {
                    valorCpf = "999999999";
                }
                else
                {
                    valorCpf = cpf.Valor;
                }

            }

            return new Field(
                "cpfCnpj",
                valorCpf,
                Field.Store.YES,
                Field.Index.ANALYZED);
        }

        private Field GetAssuntoDocumento(Documento content)
        {
            return new Field(
               "assunto",
               content.Assunto,
               Field.Store.NO,
               Field.Index.ANALYZED);
        }

        private Field GetContentTypeField(int contentType)
        {
            return new Field(
                "contentType",
                contentType.ToString(),
                Field.Store.YES,
                Field.Index.ANALYZED);
        }

        private Field GetIndexacaoField(IList<Indexacao> indexacao)
        {
            var valores = string.Empty;

            foreach (var index in indexacao)
            {
                valores += " " + index.Valor;
            }

            return new Field(
                "indexacao",
                valores,
                Field.Store.YES,
                Field.Index.ANALYZED);
        }

        private Field GetContentIdField(Documento content)
        {
            return new Field(
                "documentoId",
                content.Id.ToString(),
                Field.Store.YES,
                Field.Index.NOT_ANALYZED);
        }

        private Field GetTipoDocumento(Documento content)
        {
            return new Field(
                "tipoDocumentoId",
                content.TipoDocumento.Id.ToString(),
                Field.Store.YES,
                Field.Index.NOT_ANALYZED);
        }

        private Field GetDataCriacao(Documento content)
        {
            var dateValue = DateTools.DateToString(
                content.DataCriacao,
                DateTools.Resolution.DAY);

            return new Field(
                "dataCriacao",
                dateValue, 
                Field.Store.NO, 
                Field.Index.NOT_ANALYZED);
        }
    }
}
