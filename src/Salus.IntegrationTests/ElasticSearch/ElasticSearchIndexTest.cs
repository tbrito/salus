using NUnit.Framework;
using Salus.Infra.ElasticSearch;
using Salus.Model.Entidades;
using System;
using System.Collections.Generic;

namespace Salus.IntegrationTests.ElasticSearch
{
    [TestFixture]
    public class ElasticSearchIndexTest
    {
        [Test]
        public void DeveIndexarUmDocumento()
        {
            var salusSearch = new SalusElasticSearch();
            var tiago = new Usuario { Id = 57, Nome = "Tiago Sousa Brito" };

            var indexacao = new List<Indexacao>
            {
                new Indexacao { Valor = "2000841 df" },
                new Indexacao { Valor = "01/2016" },
                new Indexacao { Valor = "The look of love" }
            };

            var carta = new TipoDocumento { Id = 8, Nome = "Carta" };

            var documento = new Documento
            {
                Id = 1324546,
                Assunto = "Teste de Indexacao",
                CpfCnpj = "70638373115",
                DataCriacao = DateTime.Parse("01/12/1998"),
                Indexacao = indexacao,
                Usuario = tiago,
                TipoDocumento = carta
            };

            salusSearch.Indexar(documento);
        }

        [Test]
        public void DeveProcuararPorData()
        {
            var salusSearch = new SalusElasticSearch();
            var documentos = salusSearch.Pesquisar("70638373115");
        }
    }
}
