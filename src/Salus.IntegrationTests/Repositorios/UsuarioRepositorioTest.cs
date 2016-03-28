namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;

    [TestClass]
    public class UsuarioRepositorioTest : TesteDeRepositorio<Usuario, UsuarioRepositorio>
    {
        public override Usuario CriarEntidade()
        {
            return new Usuario
            {
                Email = "tiago.sousa.brito@gmail.com",
                Nome = "tbrito",
                Senha = "pwd123"
            };
        }
        
        [TestMethod]
        public void Teste()
        {
        }
    }
}
