namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WorkflwoRepositorioTest : TesteDeRepositorio<Workflow, WorkflowRepositorio>
    {
        public override Workflow CriarEntidade()
        {
            var tiago = new Usuario
            {
                Nome = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            var flavia = new Usuario
            {
                Nome = "flavia",
                Email = "flavia.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            var cartilha = new Documento
            {
                Assunto = "Cartinha pra mimha amada",
                DataCriacao = DateTime.Parse("01/02/2016"),
                Tamanho = 12554,
                Usuario = tiago,
            }.Persistir();

            return new Workflow
            {
                CriadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                De = tiago,
                Para = flavia,
                Documento = cartilha,
                Mensagem = "Oi pra voce!",
                Status = WorkflowStatus.EmFluxo
            };
        }
    }
}
