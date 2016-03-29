namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;
    using System;

    [TestClass]
    public class TrilhaRepositorioTest : TesteDeRepositorio<Trilha, TrilhaRepositorio>
    {
        public override Trilha CriarEntidade()
        {
            return new Trilha
            {
                Data = DateTime.Parse("01/02/2016"),
                Descricao = "Usuario Criado com sucesso",
                Recurso = "api/usuario/adicionar",
                Tipo = TipoTrilha.Criacao,
                Usuario = new Usuario { Id = 1 }
            };
        }

        [TestMethod]
        public void Teste()
        {
        }
    }
}
