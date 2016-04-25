namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model;
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
                Usuario = tiago
            };
        }

        [Test]
        public void DeveAlterarStatusDePesquisaDoDocumento()
        {
            var tiago = new Usuario
            {
                Nome = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            var documento = new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
                SearchStatus = SearchStatus.CantIndex
            }.Persistir();

            this.repositorio.AlterStatus(documento.Id, SearchStatus.DontIndex);

            this.ResetarConexao();

            var documentoAlterado = this.repositorio.ObterPorId(documento.Id);

            Assert.AreEqual(documentoAlterado.SearchStatus, SearchStatus.DontIndex);
        }

        [Test]
        public void DeveObterDocumentoParaIndexarPesquisaComIndexacao()
        {
            var tiago = new Usuario
            {
                Nome = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            var documento = new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
                SearchStatus = SearchStatus.ToIndex
            }.Persistir();

            var contrato = new TipoDocumento
            {
                Nome = "contrato",
                Parent = null
            }.Persistir();

            new Indexacao
            {
                Documento = documento,
                Chave = new Chave { Nome = "chave1", TipoDocumento = contrato, TipoDado = TipoDado.Texto}.Persistir(),
                Valor = "valor1"
            }.Persistir();

            new Indexacao
            {
                Documento = documento,
                Chave = new Chave { Nome = "chave2", TipoDocumento = contrato, TipoDado = TipoDado.Texto }.Persistir(),
                Valor = "valor2"
            }.Persistir();

            this.ResetarConexao();

            var documentos = this.repositorio.ObterTodosParaIndexar(1);
            
            Assert.AreEqual(documentos[0].Indexacao.Count, 2);
        }

        [Test]
        public void DeveObterPorListaDeIds()
        {
            var tiago = new Usuario
            {
                Nome = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            var documento1 = new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
                SearchStatus = SearchStatus.ToIndex
            }.Persistir();

            var documento2 = new Documento
            {
                Assunto = "Assunto do documento",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
                SearchStatus = SearchStatus.ToIndex
            }.Persistir();

            var contrato = new TipoDocumento
            {
                Nome = "contrato",
                Parent = null
            }.Persistir();

            new Indexacao
            {
                Documento = documento1,
                Chave = new Chave { Nome = "chave1", TipoDocumento = contrato, TipoDado = TipoDado.Texto }.Persistir(),
                Valor = "valor1"
            }.Persistir();

            new Indexacao
            {
                Documento = documento2,
                Chave = new Chave { Nome = "chave2", TipoDocumento = contrato, TipoDado = TipoDado.Texto }.Persistir(),
                Valor = "valor2"
            }.Persistir();

            this.ResetarConexao();

            int[] ids = new[]
            {
                documento1.Id,
                documento2.Id
            };

            var documentos = this.repositorio.ObterPorIdsComTipoDocumentoEIndexacoes(ids);

            Assert.AreEqual(documentos.Count, 2);
        }
    }
}
