namespace Salus.Model.Servicos
{
    using System;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class IniciarAtendimentoMeditoServico
    {
        private readonly IRegistroAdentimentoRepositorio registroAdentimentoRepositorio;
        private readonly IRelogio relogio;

        public IniciarAtendimentoMeditoServico(
            IRegistroAdentimentoRepositorio registroAdentimentoRepositorio, 
            IRelogio relogio)
        {
            this.registroAdentimentoRepositorio = registroAdentimentoRepositorio;
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

            this.registroAdentimentoRepositorio.Salvar(registroAtendimento);
        }
    }
}