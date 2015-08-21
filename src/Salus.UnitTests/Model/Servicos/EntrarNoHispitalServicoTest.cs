using System;
using System.Security.Authentication;

namespace Salus.UnitTests.Model.Servicos
{
    using Xunit;
    using Rhino.Mocks;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;

    public class EntrarNoHispitalServicoTest
    {
        private IRegistroAdentimentoRepositorio registroAdentimentoRepositorio;
        private IRelogio relogio;

        public EntrarNoHispitalServicoTest()
        {
            this.registroAdentimentoRepositorio = MockRepository.GenerateStub<IRegistroAdentimentoRepositorio>();
            this.relogio = MockRepository.GenerateStub<IRelogio>();
        }

        [Fact]
        public void DeveRegistrarEntradaNoHospital()
        {
            var registroAtendimento = new RegistroAtendimento();

            this.relogio
                .Stub(x => x.Agora())
                .Return(new DateTime(2015, 08, 21, 15, 21, 11));

            this.registroAdentimentoRepositorio
                .Stub(x => x.JaExisteRegistroEmAberto(registroAtendimento))
                .Return(false);

            this.registroAdentimentoRepositorio
                .Stub(x => x.ObterAtendimentoFinalizado())
                .Return(null);

            var entrarNoHospital = new EntrarNoHispitalServico(
                this.registroAdentimentoRepositorio,
                this.relogio);

            entrarNoHospital.Executar(registroAtendimento);

            registroAdentimentoRepositorio.AssertWasCalled(x => x.Salvar(registroAtendimento));
            relogio.AssertWasCalled(x => x.Agora());
        }

        [Fact]
        public void DeveApagarRegistroDeEntradaCasoEstejaFinalizado()
        {
            var registroAtendimento = new RegistroAtendimento();
            var registroAtendimentoFinalizado = new RegistroAtendimento();

            this.relogio
                .Stub(x => x.Agora())
                .Return(new DateTime(2015, 08, 21, 15, 21, 11));

            this.registroAdentimentoRepositorio
                .Stub(x => x.JaExisteRegistroEmAberto(registroAtendimento))
                .Return(false);

            this.registroAdentimentoRepositorio
                .Stub(x => x.ObterAtendimentoFinalizado())
                .Return(registroAtendimentoFinalizado);

            var entrarNoHospital = new EntrarNoHispitalServico(
                this.registroAdentimentoRepositorio,
                this.relogio);

            entrarNoHospital.Executar(registroAtendimento);

            registroAdentimentoRepositorio.AssertWasCalled(x => x.Excluir(registroAtendimentoFinalizado));
            registroAdentimentoRepositorio.AssertWasCalled(x => x.Salvar(registroAtendimento));
            relogio.AssertWasCalled(x => x.Agora());
        }

        [Fact]
        public void DeveDispararExcecaoCasoUsuarioJaTenhaRegistradoUmaEntrada()
        {
            var registroAtendimento = new RegistroAtendimento();

            this.relogio
                .Stub(x => x.Agora())
                .Return(new DateTime(2015, 08, 21, 15, 21, 11));

            this.registroAdentimentoRepositorio
                .Stub(x => x.JaExisteRegistroEmAberto(registroAtendimento))
                .Return(true);

            var entrarNoHospital = new EntrarNoHispitalServico(
                this.registroAdentimentoRepositorio,
                this.relogio);

            var ex = Assert.Throws<Exception>(() => entrarNoHospital.Executar(registroAtendimento));

            Assert.Equal("Já existe um registro de entrada neste hospital", ex.Message);
        }
    }
}
