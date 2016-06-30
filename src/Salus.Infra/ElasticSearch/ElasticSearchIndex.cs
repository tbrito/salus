using Nest;
using Salus.Model.Entidades;
using System;

namespace Salus.Infra.ElasticSearch
{
    public class ElasticSearchIndex
    {
        public void Indexar(SearchObject indexacao)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            client.Index(indexacao);
        }
    }
}
