using Salus.Model.Entidades;

namespace Salus.Model.Repositorios
{
    public interface IStorageRepositorio : IRepositorio<Storage>
    {
        Storage ObterPorSalusId(string salusId);
    }
}
