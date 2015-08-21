using System.Collections.Generic;
using System.Linq;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class RankingHospitaisServico
    {
        private readonly IRegistroAdentimentoRepositorio registroAdentimentoRepositorio;
        private readonly IRelogio relogio;

        public RankingHospitaisServico(
            IRegistroAdentimentoRepositorio registroAdentimentoRepositorio, 
            IRelogio relogio)
        {
            this.registroAdentimentoRepositorio = registroAdentimentoRepositorio;
            this.relogio = relogio;
        }

        public Ranking ObterPorMenorTempo(Atendimento atendimento, Especialidade especialidade)
        {
            var registros = this.registroAdentimentoRepositorio.ObterPorTipoAtendimentoEEspecialidade(
                atendimento, especialidade);

            var ranking = new Ranking();
            var rankingTemp = new Ranking();

            foreach (var registro in registros)
            {
                if (registro.InicioAtendimentoMedico == null)
                {
                    registro.InicioAtendimentoMedico = this.relogio.Agora();
                }

                var tempoNoHospital = registro.InicioAtendimentoMedico.GetValueOrDefault()
                    .Subtract(registro.EntradaNoHospital.GetValueOrDefault());

                var hospital = registro.Hospital;
                hospital.TempoAtendimento = tempoNoHospital.Minutes;

                rankingTemp.Hospitais.Add(hospital);
            }

            var deptRpt2 = from hospital in rankingTemp.Hospitais
                           group hospital by hospital into grp
                           select new
                           {
                               Hospital = grp.Key,
                               AverageMarks = grp.Average(ed => ed.TempoAtendimento)
                           };

            foreach (var lista in deptRpt2.OrderBy(x => x.AverageMarks))
            {
                ranking.Hospitais.Add(lista.Hospital);    
            }

            return ranking;
        }
    }
}