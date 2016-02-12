namespace Salus.IntegrationTests
{
    using NUnit.Framework;
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;

    [TestFixture]
    public abstract class TesteDeRepositorio<TEntidade, TRepositorio> : TesteAutomatizado
        where TRepositorio : Repositorio<TEntidade>, new()
        where TEntidade : Entidade, new()
    {
        protected TRepositorio repositorio = new TRepositorio();

        [Test]
        [Category("TesteRepositorio")]
        public void DeveIncluir()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            Assert.AreNotEqual(entidade.Id, 0);
        }

        [Test]
        [Category("TesteRepositorio")]
        public void DeveObterPorId()
        {
            var entidade = this.CriarEntidade();
            this.repositorio.Salvar(entidade);
            
            var novaEntidade = this.repositorio.ObterPorId(entidade.Id);

            Assert.AreEqual(entidade.Id, novaEntidade.Id);
        }

        [Test]
        [Category("TesteRepositorio")]
        public void DeveObterTodos()
        {
            var entidade = this.CriarEntidade();
            var entidade2 = this.CriarEntidade();

            this.repositorio.Salvar(entidade);
            this.repositorio.Salvar(entidade2);
            
            var novasEntidades = this.repositorio.ObterTodos();

            Assert.AreEqual(novasEntidades.Count, 2);
        }
        
        [TearDown]
        public void DepoisDoTest()
        {
            this.repositorio.ApagarTodos();
        }

        public abstract TEntidade CriarEntidade();
    }
}