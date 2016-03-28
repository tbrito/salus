namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;
    using System;

    [TestClass]
    public class DocumentoRepositorioTest : TesteDeRepositorio<Documento, DocumentoRepositorio>
    {
        public override Documento CriarEntidade()
        {
            return new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554
            };
        }

        [TestMethod]
        public void Teste()
        {
        }
    }
}
