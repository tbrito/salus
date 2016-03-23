using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Infra.Repositorios
{
    public class StorageRepositorio : Repositorio<Storage>, IStorageRepositorio
    {
        public Storage ObterPorDocumentoId(int documentoId)
        {
            return this.Sessao.QueryOver<Storage>()
                .Where(x => x.Documento.Id == documentoId)
                .SingleOrDefault();
        }
    }
}
