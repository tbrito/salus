namespace Salus.Model.Search
{
    using System.Collections.Generic;

    public class ResultadosPesquisa
    {
        private readonly int[] documentoIds;

        public ResultadosPesquisa(int[] documentoIds, int resultadosPorPagina, int paginaAtual, int total)
        {
            this.documentoIds = documentoIds;
            this.Paging = new Paging(resultadosPorPagina, paginaAtual, total);
            this.Total = total;
        }

        public IList<int> DocumentoIds
        {
            get { return this.documentoIds; }
        }

        public Paging Paging
        {
            get;
            private set;
        }

        public int Total
        {
            get;
            private set;
        }

        public bool ExisteResultado
        {
            get
            {
                return this.documentoIds.Length > 0;
            }
        }
        
        public bool TemPaginaAnterior
        {
            get { return this.ExisteResultado && this.Paging.ActualPage > 1; }
        }

        public bool TemProximaPagina
        {
            get { return this.ExisteResultado && this.Paging.ActualPage < this.Paging.Pages; }
        }

        public static ResultadosPesquisa Vazio()
        {
            return new ResultadosPesquisa(new int[] { }, 0, 0, 0);
        }

        public IEnumerable<int> ObterDocumentosDaPagina(int page)
        {
            for (var i = this.Paging.StartIndex; i <= this.Paging.EndIndex; i++)
            {
                yield return this.documentoIds[i];
            }
        }
    }
}