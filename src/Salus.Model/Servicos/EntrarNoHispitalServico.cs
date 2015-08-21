using System;

namespace Salus.Model.Servicos
{
    using Repositorios;
    using Entidades;

    public class EntrarNoHispitalServico
    {
        private readonly IRegistroAdentimentoRepositorio registroAtendimentoRepositorio;
        private readonly IRelogio relogio;

        public EntrarNoHispitalServico(
            IRegistroAdentimentoRepositorio registroAtendimentoRepositorio,
            IRelogio relogio)
        {
            this.registroAtendimentoRepositorio = registroAtendimentoRepositorio;
            this.relogio = relogio;
        }

        public void Executar(RegistroAtendimento registroAtendimento)
        {
            var jaExisteRegistro = this.registroAtendimentoRepositorio.JaExisteRegistroEmAberto(registroAtendimento);

            if (jaExisteRegistro)
            {
                throw new Exception("Já existe um registro de entrada neste hospital");
            }

            var registroFinalizado = this.registroAtendimentoRepositorio.ObterAtendimentoFinalizado();

            if (registroFinalizado != null)
            {
                this.registroAtendimentoRepositorio.Excluir(registroFinalizado);  
            }

            registroAtendimento.EntradaNoHospital = this.relogio.Agora();
            
            this.registroAtendimentoRepositorio.Salvar(registroAtendimento);
        }
    }
}
