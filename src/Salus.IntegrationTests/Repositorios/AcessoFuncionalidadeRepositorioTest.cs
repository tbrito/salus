namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AcessoFuncionalidadeRepositorioTest : TesteDeRepositorio<AcessoFuncionalidade, AcessoFuncionalidadeRepositorio>
    {
        public override AcessoFuncionalidade CriarEntidade()
        {
            return new AcessoFuncionalidade
            {
                AtorId = 1,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.CaixaEntrada
            };
        }

        [Test]
        public void DeveObterPorPapelEPorAtorId()
        {
            var administrador = new Perfil
            {
                Nome = "administrador"
            }.Persistir();

            var departamentoPessoal = new Area
            {
                Nome = "DepartamentoPessoal",
                Abreviacao = "DP",
                Parent = null
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.CaixaEntrada
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.ConfiguracaoArea
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = departamentoPessoal.Id,
                Papel = Papel.Area,
                Funcionalidade = Funcionalidade.ConfiguracaoArea
            }.Persistir();

            this.ResetarConexao();

            var funcionalidades = this.repositorio.ObterPorPapelComAtorId(Papel.Pefil.Value, administrador.Id);

            Assert.AreEqual(funcionalidades.Count, 2);
        }

        [Test]
        public void DeveApagarPermissoesDeUmAtorId()
        {
            var administrador = new Perfil
            {
                Nome = "administrador"
            }.Persistir();

            var departamentoPessoal = new Area
            {
                Nome = "DepartamentoPessoal",
                Abreviacao = "DP",
                Parent = null
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.CaixaEntrada
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.ConfiguracaoArea
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = departamentoPessoal.Id,
                Papel = Papel.Area,
                Funcionalidade = Funcionalidade.ConfiguracaoArea
            }.Persistir();

            this.ResetarConexao();

            this.repositorio.ApagarAcessosDoAtor(Papel.Pefil.Value, administrador.Id);

            this.ResetarConexao();

            var acessos = this.repositorio.ObterPorPapelComAtorId(Papel.Pefil.Value, administrador.Id);
            Assert.AreEqual(acessos.Count, 0);

            this.ResetarConexao();

            acessos = this.repositorio.ObterPorPapelComAtorId(Papel.Area.Value, departamentoPessoal.Id);
            Assert.AreEqual(acessos.Count, 1);
        }

        [Test]
        public void DeveObterPermissoesDeUmUsuario()
        {
            var administrador = new Perfil
            {
                Nome = "administrador"
            }.Persistir();

            var departamentoPessoal = new Area
            {
                Nome = "DepartamentoPessoal",
                Abreviacao = "DP",
                Parent = null
            }.Persistir();

            var financeiro = new Area
            {
                Nome = "Financeiro",
                Abreviacao = "FIN",
                Parent = null
            }.Persistir();

            var tiago = new Usuario
            {
                Login = "tbrito",
                Area = departamentoPessoal,
                Perfil = administrador,
                Senha = "pwd",
                Email = "tiago@gmail",
            }.Persistir();
            
            new AcessoFuncionalidade
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.CaixaEntrada
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = departamentoPessoal.Id,
                Papel = Papel.Area,
                Funcionalidade = Funcionalidade.ConfiguracaoArea
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = tiago.Id,
                Papel = Papel.Usuario,
                Funcionalidade = Funcionalidade.ConfiguracaoTipoDocumento
            }.Persistir();

            new AcessoFuncionalidade
            {
                AtorId = financeiro.Id,
                Papel = Papel.Area,
                Funcionalidade = Funcionalidade.ConfiguracaoAcessoFuncionalidades
            }.Persistir();

            this.ResetarConexao();

            var acessos = this.repositorio.ObterDoUsuario(tiago);

            Assert.AreEqual(acessos.Count, 3);
        }
    }
}
