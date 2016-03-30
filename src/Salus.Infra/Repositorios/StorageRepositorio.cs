using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Infra.Repositorios
{
    public class StorageRepositorio : Repositorio<Storage>, IStorageRepositorio
    {
        public Storage ObterPorSalusId(string salusId)
        {
            return this.Sessao.QueryOver<Storage>()
                .Where(x => x.SalusId == salusId)
                .SingleOrDefault();
        }
    }
}
