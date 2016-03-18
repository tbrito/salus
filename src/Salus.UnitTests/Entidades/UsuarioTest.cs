namespace Salus.UnitTests.Entidades
{
    using NUnit.Framework;
    using Salus.Model.Entidades;

    [TestFixture]
    public class UsuarioTest : TesteDePoco<Usuario>
    {
        [Test]
        public void DeveCriarUsuarioComPerfilAdministrador()
        {
            var tiago = new Usuario();
            var administrador = new Perfil();

            tiago.Perfil = administrador;
        }
    }
}
