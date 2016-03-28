namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entidades;
    using System;

    [TestClass]
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
        
        [TestMethod]
        public void Teste()
        {
        }
    }
}
