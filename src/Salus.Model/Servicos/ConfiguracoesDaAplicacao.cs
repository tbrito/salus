using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class ConfiguracoesDaAplicacao : IConfiguracoesDaAplicacao
    {
        private IConfiguracaoRepositorio configuracaoRepositorio;

        public string CaminhoIndicePesquisa()
        {
            var configuracao = this.configuracaoRepositorio.ObterPorChave("pesquisa.diretorio.indice");
            return configuracao.Valor;
        }
    }
}
