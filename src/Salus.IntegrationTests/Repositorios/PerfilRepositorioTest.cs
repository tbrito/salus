namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;

    [TestFixture]
    public class PerfilRepositorioTest : TesteDeRepositorio<Perfil, PerfilRepositorio>
    {
        public override Perfil CriarEntidade()
        {
            return new Perfil
            {
                Ativo = true,
                Nome = "Administrador"
            };
        }
    }
}
