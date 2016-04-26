namespace Salus.Infra.Repositorios
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class ConfiguracaoRepositorio : Repositorio<Configuracao>, IConfiguracaoRepositorio
    {
        public Configuracao ObterPorChave(string chave)
        {
            return this.Sessao.QueryOver<Configuracao>()
                .Where(x => x.Chave == chave)
                .SingleOrDefault();
        }
    }
}