namespace Salus.UnitTests.Model.Servicos
{
    using System;
    using System.Collections.Generic;
    using Rhino.Mocks;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Xunit;

    public class RankingHospitaisServicoTest
    {
        private IRegistroAdentimentoRepositorio registroAdentimentoRepositorio;
        private IRelogio relogio;

        public RankingHospitaisServicoTest()
        {
            this.registroAdentimentoRepositorio = MockRepository.GenerateStub<IRegistroAdentimentoRepositorio>();
            this.relogio = MockRepository.GenerateStub<IRelogio>();
        }

        [Fact]
        public void DeveRetornarHospitaisPorOrdemDeMenorTempoDeEspera()
        {
            var rakingHospitalServico = new RankingHospitaisServico(this.registroAdentimentoRepositorio, this.relogio);
            var ortopedia = new Especialidade();
            var santaMarta = new Hospital() { Nome = "SantaMarta" };
            var anchieta = new Hospital() { Nome = "Anchieta" };
            var alvorada = new Hospital() { Nome = "Alvorada" };

            var atendimentoAnchieta1 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 09:00"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 09:30"),
                Hospital = anchieta,
                Especialidade = ortopedia
            };

            var atendimentoAnchieta2 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 09:30"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 10:00"),
                Hospital = anchieta,
                Especialidade = ortopedia
            };

            var atendimentoSantaMarta1 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 08:00"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 08:20"),
                Hospital = santaMarta,
                Especialidade = ortopedia
            };

            var atendimentoSantaMarta2 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 09:00"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 09:20"),
                Hospital = santaMarta,
                Especialidade = ortopedia
            };

            var atendimentoAlvorada1 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 08:30"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 09:10"),
                Hospital = alvorada,
                Especialidade = ortopedia
            };

            var atendimentoAlvorada2 = new RegistroAtendimento
            {
                EntradaNoHospital = DateTime.Parse("21/08/2015 09:30"),
                InicioAtendimentoMedico = DateTime.Parse("21/08/2015 10:10"),
                Hospital = alvorada,
                Especialidade = ortopedia
            };

            IList<RegistroAtendimento> atendimentos = new List<RegistroAtendimento>
            {
                atendimentoAnchieta1,
                atendimentoAnchieta2,
                atendimentoSantaMarta1,
                atendimentoSantaMarta2,
                atendimentoAlvorada1,
                atendimentoAlvorada2,
            };

            this.registroAdentimentoRepositorio
                .Stub(x => x.ObterPorTipoAtendimentoEEspecialidade(Atendimento.Infantil, ortopedia))
                .Return(atendimentos);

            this.relogio
                .Stub(x => x.Agora())
                .Return(DateTime.Parse("21/08/2015 11:00"));

            var ranking = rakingHospitalServico.ObterPorMenorTempo(Atendimento.Infantil, ortopedia);

            Assert.Equal(santaMarta, ranking.Hospitais[0]);
            Assert.Equal(20, ranking.Hospitais[0].TempoAtendimento);

            Assert.Equal(anchieta, ranking.Hospitais[1]);
            Assert.Equal(30, ranking.Hospitais[1].TempoAtendimento);

            Assert.Equal(alvorada, ranking.Hospitais[2]);
            Assert.Equal(40, ranking.Hospitais[2].TempoAtendimento);
        }
    }
}