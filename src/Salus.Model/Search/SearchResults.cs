namespace Salus.Model.Search
{
    using System.Collections.Generic;

    public class SearchResults
    {
        private readonly int[] contentIds;

        public SearchResults(int[] contentIds, int resultsPerPage, int actualPage, int total)
        {
            this.contentIds = contentIds;
            this.Paging = new Paging(resultsPerPage, actualPage, total);
            this.Total = total;
        }

        public IList<int> ContentIds
        {
            get { return this.contentIds; }
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

        public bool ExistResult
        {
            get
            {
                return this.contentIds.Length > 0;
            }
        }
        
        public bool HasPreviusPage
        {
            get { return this.ExistResult && this.Paging.ActualPage > 1; }
        }

        public bool HasNextPage
        {
            get { return this.ExistResult && this.Paging.ActualPage < this.Paging.Pages; }
        }

        public static SearchResults EmptyResult()
        {
            return new SearchResults(new int[] { }, 0, 0, 0);
        }

        public IEnumerable<int> GetContentsOfPage(int page)
        {
            for (var i = this.Paging.StartIndex; i <= this.Paging.EndIndex; i++)
            {
                yield return this.contentIds[i];
            }
        }
    }
}