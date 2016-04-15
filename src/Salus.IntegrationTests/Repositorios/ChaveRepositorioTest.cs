namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model;
    using Model.Entidades;
    using NUnit.Framework;

    [TestFixture]
    public class ChaveRepositorioTest : TesteDeRepositorio<Chave, ChaveRepositorio>
    {
        public override Chave CriarEntidade()
        {
            var carta = new TipoDocumento
            {
                Ativo = true,
                EhPasta = false,
                Nome = "Carta",
                Parent = null
            }.Persistir();

            return new Chave
            {
               Ativo = true,
               Nome = "cpf/cnpj",
               Obrigatorio = true,
               TipoDado = TipoDado.CpfCnpj,
               Mascara = "213423423@#$@#$",
               TipoDocumento = carta
            };
        }
    }
}
