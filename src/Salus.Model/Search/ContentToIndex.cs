namespace Salus.Model.Search
{
    using System;
    
    public class ContentToIndex : Entidade
    {
        public string Indexes;
        public int CategoryId;
        public DateTime? CreatedAt;
        public string Subject;
        public int ContentType;
    }
}