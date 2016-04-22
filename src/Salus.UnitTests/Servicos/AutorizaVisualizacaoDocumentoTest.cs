using NUnit.Framework;
using Rhino.Mocks;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using Salus.Model.Servicos;
using System.Collections.Generic;

namespace Salus.UnitTests.Servicos
{
    [TestFixture]
    public class AutorizaVisualizacaoDocumentoTest
    {
        private Usuario tiago;

        [SetUp]
        public void Setup()
        {
            tiago = new Usuario
            {
                Id = 1,
                Area = new Area { Segura = true },
                Perfil = new Perfil(),
            };
        }

        [Test]
        public void ProprietarioDoDocumentoTemAcessoAoDocumento()
        {
            var usuarioRepositorio = MockRepository.GenerateStub<ISessaoDoUsuario>();
            var autorizacao = new AutorizaVisualizacaoDocumento(usuarioRepositorio);

            usuarioRepositorio.Stub(x => x.UsuarioAtual).Return(tiago);

            var acessos = new List<AcessoDocumento>();

            var documento = new Documento
            {
                Usuario = tiago
            };

            var podeAcessar = autorizacao.Executar(acessos, documento);

            Assert.AreEqual(podeAcessar, true);
        }
    }
}
