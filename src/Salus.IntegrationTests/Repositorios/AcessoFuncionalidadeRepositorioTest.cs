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
    }
}
