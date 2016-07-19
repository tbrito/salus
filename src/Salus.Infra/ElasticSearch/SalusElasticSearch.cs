namespace Salus.Infra.ElasticSearch
{
    using Model.Entidades;
    using Nest;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SalusElasticSearch
    {
        private ElasticClient client;

        public SalusElasticSearch()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("my-application");

            this.client = new ElasticClient(settings);
        }

        public void Indexar(Documento content)
        {
            this.client.Index(content);
        }

        public IList<Documento> Pesquisar(string termo)
        {
            var searchResults = this.client.Search<Documento>(x =>x
                .From(0)
                .Size(10)
                .Query(q => q
                    .Term(i => i.CpfCnpj, termo)));

            return searchResults.Documents as IList<Documento>;
        }
    }
}
