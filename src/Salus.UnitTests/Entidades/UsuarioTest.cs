namespace Salus.UnitTests.Entidades
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Salus.Model.Entidades;

    [TestClass]
    public class UsuarioTest : TesteDePoco<Usuario>
    {
        [TestMethod]
        public void DeveCriarUsuarioComPerfilAdministrador()
        {
            var tiago = new Usuario();
            var administrador = new Perfil();

            tiago.Perfil = administrador;
        }
    }
}
