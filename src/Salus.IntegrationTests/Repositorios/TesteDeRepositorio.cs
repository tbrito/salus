namespace Salus.IntegrationTests
{
    using Boot;
    using Infra.ConnectionInfra;
    using NHibernate;
    using NUnit.Framework;
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;
    using SharpArch.NHibernate;
    using System;
    using System.IO;
    using System.Linq;
    [TestFixture]
    public class TesteDeRepositorio<TEntidade, TRepositorio> : TesteAutomatizado
        where TRepositorio : Repositorio<TEntidade>, new()
        where TEntidade : Entidade, new()
    {
        protected TRepositorio repositorio = new TRepositorio();
        private ITransaction transaction;

        [SetUp]
        public void Initialize()
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
            };

            new ClearDatabase().Execute();

            NHibernateSession.Reset();

            NHibernateSession.Init(
                new SimpleSessionStorage(),
                mappings,
                null, null, null, null, BancoDeDados.Configuration());
        }

        [TearDown]
        public void Cleanup()
        {
            this.repositorio.ApagarTodos();
            NHibernateSession.Current.Flush();
        }

        [Test]
        public void DeveIncluir()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            Assert.AreNotEqual(entidade.Id, 0);
        }

        [Test]
        public void DeveObterPorId()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);

            var novaEntidade = this.repositorio.ObterPorId(entidade.Id);

            Assert.AreEqual(entidade.Id, novaEntidade.Id);
        }

        [Test]
        public void DeveObterTodos()
        {
            var entidade = this.CriarEntidade();
            var entidade2 = this.CriarEntidade();

            this.repositorio.Salvar(entidade);
            this.repositorio.Salvar(entidade2);

            var novasEntidades = this.repositorio.ObterTodos();

            Assert.AreEqual(novasEntidades.Count, 2);
        }

        public virtual TEntidade CriarEntidade()
        {
            return new TEntidade();
        }
    }
}