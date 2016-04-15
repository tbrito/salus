namespace Salus.IntegrationTests
{
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;

    [TestFixture]
    public class TipoDocumentoRepositorioTest : TesteDeRepositorio<TipoDocumento, TipoDocumentoRepositorio>
    {
        public override TipoDocumento CriarEntidade()
        {
            return new TipoDocumento
            {
                Ativo = true,
                EhPasta = false,
                Nome = "Carta",
                Parent = null
            };
        }
    }
}
