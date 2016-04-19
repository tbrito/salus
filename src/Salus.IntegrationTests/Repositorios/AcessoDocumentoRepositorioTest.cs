namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AcessoDocumentoRepositorioTest : TesteDeRepositorio<AcessoDocumento, AcessoDocumentoRepositorio>
    {
        private TipoDocumento carta;
        private TipoDocumento oficio;
        private TipoDocumento folhaPonto;

        public override AcessoDocumento CriarEntidade()
        {
            
            return new AcessoDocumento
            {
                AtorId = 1,
                Papel = Papel.Pefil,
                TipoDocumento = this.carta
            };
        }

        [SetUp]
        public void AntesDoTeste()
        {
            this.carta = new TipoDocumento
            {
                Nome = "carta",
                Ativo = true,
                Parent = null,
                EhPasta = false
            }.Persistir();

            this.oficio = new TipoDocumento
            {
                Nome = "oficio",
                Ativo = true,
                Parent = null,
                EhPasta = false
            }.Persistir();

            this.folhaPonto = new TipoDocumento
            {
                Nome = "folha",
                Ativo = true,
                Parent = null,
                EhPasta = false
            }.Persistir();

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

            new AcessoDocumento
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                TipoDocumento = carta
            }.Persistir();

            new AcessoDocumento
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                TipoDocumento = this.oficio
            }.Persistir();

            new AcessoDocumento
            {
                AtorId = departamentoPessoal.Id,
                Papel = Papel.Area,
                TipoDocumento = this.folhaPonto
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

            new AcessoDocumento
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                TipoDocumento = this.carta
            }.Persistir();

            new AcessoDocumento
            {
                AtorId = administrador.Id,
                Papel = Papel.Pefil,
                TipoDocumento = this.oficio
            }.Persistir();

            new AcessoDocumento
            {
                AtorId = departamentoPessoal.Id,
                Papel = Papel.Area,
                TipoDocumento = this.folhaPonto
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
    }
}
