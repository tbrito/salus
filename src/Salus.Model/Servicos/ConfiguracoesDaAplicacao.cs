using Salus.Model.Repositorios;
using System;

namespace Salus.Model.Servicos
{
    public class ConfiguracoesDaAplicacao : IConfiguracoesDaAplicacao
    {
        private IConfiguracaoRepositorio configuracaoRepositorio;
        
        public ConfiguracoesDaAplicacao(IConfiguracaoRepositorio configuracaoRepositorio)
        {
            this.configuracaoRepositorio = configuracaoRepositorio;
        }

        public int MaximoResultadoPorPagina
        {
            get
            {
                return Convert.ToInt32(this.configuracaoRepositorio
                    .ObterPorChave("pesquisa.resultado.maximo.pagina")
                    .Valor);
            }
        }

        public int ResultadoMaximoConsulta
        {
            get
            {
                return Convert.ToInt32(this.configuracaoRepositorio
                    .ObterPorChave("pesquisa.resultado.maximo")
                    .Valor);
            }
        }

        public string CaminhoIndicePesquisa()
        {
            var configuracao = this.configuracaoRepositorio.ObterPorChave("pesquisa.diretorio.indice");
            return configuracao.Valor;
        }
    }
}
