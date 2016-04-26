using NUnit.Framework;
using Salus.Infra.DataAccess;
using Salus.Infra.IoC;

namespace Salus.IntegrationTests.DataAccess
{
    [TestFixture]
    public class UnidadeDeTrabalhoTest
    {
        [Test]
        public void DeveIniciarUnidadeDeTrabalho()
        {
            var unidadeDeTrabalho = new UnidadeDeTrabalho();

            for (int i = 0; i < 10; i++)
            {
                using (var unidade = unidadeDeTrabalho.Iniciar())
                {
                    Assert.IsTrue(unidade.EstaAberta);
                } 
            }
        }
    }
}
