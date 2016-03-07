namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
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
        
        [Test]
        public void Teste()
        {
        }
    }
}
