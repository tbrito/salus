namespace Salus.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Infra.Repositorios;
    using Model.Entidades;
    [TestClass]
    public class UsuarioRepositorioTest : TesteAutomatizado
    {
        [TestMethod]
        public void DeveIniciarCriarUsuario()
        {
            var usuarioRepositorio = new UsuarioRepositorio();

            var usuario = new Usuario();
            usuario.Nome = "tiago";
            usuario.Email = "tiago.sousa.brito@gmail.com";
            
            usuarioRepositorio.SalvarComSenha(usuario, "pwd123");

            Assert.AreNotEqual(usuario.Id, 0);
        }
    }
}
