namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;

    [TestFixture]
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
        
        [Test]
        public void Teste()
        {
        }
    }
}
