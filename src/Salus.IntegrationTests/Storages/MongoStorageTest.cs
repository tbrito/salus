using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salus.Infra.Storages;
using System;
using System.IO;

namespace Salus.IntegrationTests.Storages
{
    [TestClass]
    public class MongoStorageTest
    {
        [TestMethod]
        public void DeveInserirUmConteudoNoStorage()
        {
            var arquivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Cenarios\\Arquivos",
                "Arquivo.txt");

            var storage = new MongoStorage();
            var objectId = storage.AdicionarOuAtualizar(arquivo);

            var arquivoDoMongo = storage.Obter(objectId, "txt");

            Assert.IsTrue(File.Exists(arquivoDoMongo));
        }
    }
}
