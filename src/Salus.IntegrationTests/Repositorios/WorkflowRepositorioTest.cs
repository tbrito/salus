namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WorkflwoRepositorioTest : TesteDeRepositorio<Workflow, WorkflowRepositorio>
    {
        public override Workflow CriarEntidade()
        {
            return new Workflow
            {
                CriadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
            };
        }
        
        [Test]
        public void Teste()
        {
        }
    }
}
