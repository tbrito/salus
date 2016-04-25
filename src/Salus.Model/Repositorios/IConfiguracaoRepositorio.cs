using Salus.Model.Entidades;

namespace Salus.Model.Repositorios
{
    public interface IConfiguracaoRepositorio : IRepositorio<Configuracao>
    {
        Configuracao ObterPorChave(string chave);
    }
}