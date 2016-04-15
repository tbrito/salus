namespace Salus.IntegrationTests
{
    using Extensions;
    using Infra.Repositorios;
    using Model.Entidades;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AreaRepositorioTest : TesteDeRepositorio<Area, AreaRepositorio>
    {
        public override Area CriarEntidade()
        {
            return new Area
            {
                Abreviacao = "DP",
                Nome = "Departamento Pessoal",                
                Ativo = true,
                Segura = true,
                Parent = null
            };
        }
    }
}
