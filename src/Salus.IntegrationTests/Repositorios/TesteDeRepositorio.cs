namespace Salus.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;

    [TestClass()]
    public abstract class TesteDeRepositorio<TEntidade, TRepositorio> : TesteAutomatizado
        where TRepositorio : Repositorio<TEntidade>, new()
        where TEntidade : Entidade, new()
    {
        protected TRepositorio repositorio = new TRepositorio();

        [TestMethod]
        public void DeveIncluir()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            Assert.AreNotEqual(entidade.Id, 0);
        }

        [TestMethod]
        public void DeveObterPorId()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            
            var novaEntidade = this.repositorio.ObterPorId(entidade.Id);

            Assert.AreEqual(entidade.Id, novaEntidade.Id);
        }

        [TestMethod]
        public void DeveObterTodos()
        {
            var entidade = this.CriarEntidade();
            var entidade2 = this.CriarEntidade();

            this.repositorio.Salvar(entidade);
            this.repositorio.Salvar(entidade2);
            
            var novasEntidades = this.repositorio.ObterTodos();

            Assert.AreEqual(novasEntidades.Count, 2);
        }
        
        ////[TestCleanup]
        ////public void DepoisDoTest()
        ////{
        ////    this.repositorio.ApagarTodos();
        ////}

        public abstract TEntidade CriarEntidade();
    }
}