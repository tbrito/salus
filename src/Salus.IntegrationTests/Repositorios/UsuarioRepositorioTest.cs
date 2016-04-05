namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;

    [TestClass]
    public class UsuarioRepositorioTest : TesteDeRepositorio<Usuario, UsuarioRepositorio>
    {
        public override Usuario CriarEntidade()
        {
            Area departamentoPessoal = new Area
            {
                Nome = "Departamento Pessoal",
                Abreviacao = "DP",
                Segura = true,
                Ativo = true,
            }.Persistir();

            Perfil administrador = new Perfil
            {
                Nome = "Departamento Pessoal",
                Ativo = true,
            }.Persistir();

            return new Usuario
            {
                Email = "tiago.sousa.brito@gmail.com",
                Nome = "tbrito",
                Senha = "pwd123",
                Area = departamentoPessoal,
                Perfil = administrador
            };
        }
        
        [TestMethod]
        public void Teste()
        {
        }
    }
}
