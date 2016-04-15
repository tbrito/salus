namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DocumentoRepositorioTest : TesteDeRepositorio<Documento, DocumentoRepositorio>
    {
        public override Documento CriarEntidade()
        {
            var tiago = new Usuario
            {
                Nome = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            return new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
                
            };
        }
    }
}
