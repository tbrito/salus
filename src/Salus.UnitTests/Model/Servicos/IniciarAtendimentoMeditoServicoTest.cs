namespace Salus.UnitTests.Model.Servicos
{
    using System;
    using Rhino.Mocks;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Xunit;

    public class IniciarAtendimentoMeditoServicoTest
    {
        private IRegistroAdentimentoRepositorio registroAtentimentoRepositorio;
        private IRegistroAdentimentoHistoricoRepositorio registroAtentimentoHistoricoRepositorio;
        private IRelogio relogio;

        public IniciarAtendimentoMeditoServicoTest()
        {
            this.registroAtentimentoRepositorio = MockRepository.GenerateStub<IRegistroAdentimentoRepositorio>();
            this.registroAtentimentoHistoricoRepositorio = MockRepository.GenerateStub<IRegistroAdentimentoHistoricoRepositorio>();
            this.relogio = MockRepository.GenerateStub<IRelogio>();
        }

        [Fact]
        public void DeveIniciarAtendimentoMedico()
        {
            var registroAtendimento = new RegistroAtendimento();
            var registroAtendimentoHistorico = new RegistroAtendimentoHistorico();
            var usuario = new Usuario();

            this.relogio
                .Stub(x => x.Agora())
                .Return(new DateTime(2015, 08, 21, 15, 21, 11));
            
            this.registroAtentimentoRepositorio
                .Stub(x => x.ObterRegistroEntradaDoUsuario(usuario))
                .Return(registroAtendimento);

            var iniciarAtendimentoMedico = new IniciarAtendimentoMeditoServico(
                this.registroAtentimentoRepositorio,
                this.registroAtentimentoHistoricoRepositorio,
                this.relogio);

            iniciarAtendimentoMedico.Executar(usuario);

            registroAtentimentoRepositorio.AssertWasCalled(x => x.ObterRegistroEntradaDoUsuario(usuario));
            registroAtentimentoRepositorio.AssertWasCalled(x => x.Salvar(registroAtendimento));
            registroAtentimentoHistoricoRepositorio
                .AssertWasCalled(x => x.Salvar(registroAtendimentoHistorico), opt => opt.IgnoreArguments());
        }

        [Fact]
        public void DeveDispararExcecaoCasoNaoExistaRegistroDeEntradaParaUsuario()
        {
            var usuario = new Usuario();

            this.registroAtentimentoRepositorio
                .Stub(x => x.ObterRegistroEntradaDoUsuario(usuario))
                .Return(null);

            var iniciarAtendimentoMedico = new IniciarAtendimentoMeditoServico(
                this.registroAtentimentoRepositorio,
                registroAtentimentoHistoricoRepositorio,
                this.relogio);

            var ex = Assert.Throws<Exception>(() => iniciarAtendimentoMedico.Executar(usuario));

            Assert.Equal("Usuário não indicou entrada no hospital", ex.Message);
        }
    }
}