namespace Salus.Model.Search
{
    using System;

    public class Paging
    {
        public Paging(int resultsPerPage, int actualPage, int total)
        {
            this.ThrowIfResultsPerPageIsOne(resultsPerPage);

            this.Total = total;
            this.ResultsPerPage = resultsPerPage;
            this.ActualPage = actualPage;

            this.CalculateParameters();
        }

        public int Total
        {
            get;
            private set;
        }

        public int ActualPage
        {
            get;
            private set;
        }

        public int Pages
        {
            get;
            private set;
        }

        public int StartIndex
        {
            get;
            private set;
        }

        public int EndIndex
        {
            get;
            private set;
        }

        public int ResultsPerPage
        {
            get;
            private set;
        }

        private void CalculateParameters()
        {
            this.Pages = (int)Math.Ceiling(this.Total / (double)this.ResultsPerPage);
            this.StartIndex = this.CalculateStartIndex();
            this.EndIndex = Math.Min(this.StartIndex + (this.ResultsPerPage - 1), this.Total - 1);
        }

        private int CalculateStartIndex()
        {
            if (this.ActualPage == 1)
            {
                return 0;
            }

            return (this.ActualPage - 1) * this.ResultsPerPage;
        }

        private void ThrowIfResultsPerPageIsOne(int resultsPerPage)
        {
            if (resultsPerPage <= 1)
            {
                throw new InvalidOperationException("Resultados por página deve ser maior que 1");
            }
        }
    }
}