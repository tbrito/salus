namespace Salus.Model.Search
{
    using System;
    using Veros.Framework.Modelo;

    public class ContentToIndex : Entidade
    {
        public string Indexes;
        public int CategoryId;
        public DateTime? CreatedAt;
        public string Subject;
        public int ContentType;
    }
}