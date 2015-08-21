namespace Salus.Model.Servicos
{
    using System;
    using Entidades;
    using Repositorios;

    public class IniciarAtendimentoMeditoServico
    {
        private readonly IRegistroAdentimentoRepositorio registroAdentimentoRepositorio;
        private readonly IRegistroAdentimentoHistoricoRepositorio registroAtentimentoHistoricoRepositorio;
        private readonly IRelogio relogio;

        public IniciarAtendimentoMeditoServico(
            IRegistroAdentimentoRepositorio registroAdentimentoRepositorio, 
            IRegistroAdentimentoHistoricoRepositorio registroAtentimentoHistoricoRepositorio, 
            IRelogio relogio)
        {
            this.registroAdentimentoRepositorio = registroAdentimentoRepositorio;
            this.registroAtentimentoHistoricoRepositorio = registroAtentimentoHistoricoRepositorio;
            this.relogio = relogio;
        }

        public void Executar(Usuario usuario)
        {
            var registroAtendimento = this.registroAdentimentoRepositorio.ObterRegistroEntradaDoUsuario(usuario);

            if (registroAtendimento == null)
            {
                throw new Exception("Usuário não indicou entrada no hospital");
            }

            registroAtendimento.InicioAtendimentoMedico = this.relogio.Agora();
            registroAtendimento.Finalizado = true;

            var registroAtentimentoHistorico = RegistroAtendimentoHistorico.Novo(registroAtendimento);
            this.registroAdentimentoRepositorio.Salvar(registroAtendimento);

            registroAtentimentoHistoricoRepositorio.Salvar(registroAtentimentoHistorico);
        }
    }
}