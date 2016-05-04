namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class TrilhaRepositorioTest : TesteDeRepositorio<Trilha, TrilhaRepositorio>
    {
        public override Trilha CriarEntidade()
        {
            var tiago = new Usuario
            {
                Login = "tiago",
                Email = "tiago.sousa@gmail",
                Senha = "pwd"
            }.Persistir();

            return new Trilha
            {
                Data = DateTime.Parse("01/02/2016"),
                Descricao = "Usuario Criado com sucesso",
                Recurso = "api/usuario/adicionar",
                Tipo = TipoTrilha.Criacao,
                Usuario = tiago
            };
        }
    }
}
