namespace Salus.IntegrationTests
{
    using Infra.ConnectionInfra;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;
    using SharpArch.NHibernate;
    using System;
    using System.IO;

    [TestClass()]
    public abstract class TesteDeRepositorio<TEntidade, TRepositorio>
        where TRepositorio : Repositorio<TEntidade>, new()
        where TEntidade : Entidade, new()
    {
        protected TRepositorio repositorio = new TRepositorio();

        [TestInitialize()]
        public void Initialize()
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
            };

            NHibernateSession.Init(
               new SimpleSessionStorage(),
               mappings,
               null, null, null, null, BancoDeDados.Configuration());
        }

        [ClassCleanup()]
        public void Cleanup()
        {
            NHibernateSession.CloseAllSessions();
        }

        [TestMethod()]
        public void DeveIncluir()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            Assert.AreNotEqual(entidade.Id, 0);
        }

        [TestMethod()]
        public void DeveObterPorId()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            
            var novaEntidade = this.repositorio.ObterPorId(entidade.Id);

            Assert.AreEqual(entidade.Id, novaEntidade.Id);
        }

        [TestMethod()]
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