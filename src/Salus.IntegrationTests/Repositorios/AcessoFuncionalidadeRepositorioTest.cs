namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AcessoFuncionalidadeRepositorioTest : TesteDeRepositorio<AcessoFuncionalidade, AcessoFuncionalidadeRepositorio>
    {
        public override AcessoFuncionalidade CriarEntidade()
        {
            return new AcessoFuncionalidade
            {
                AtorId = 1,
                Papel = Papel.Pefil,
                Funcionalidade = Funcionalidade.CaixaEntrada
            };
        }
    }
}
