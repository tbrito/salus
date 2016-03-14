using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class StorageServico
    {
        private IMongoStorage gridStorage;
        private IStorageRepositorio storageRepository;

        public StorageServico(
            IMongoStorage gridStorage,
            IStorageRepositorio storageRepository)
        {
            this.gridStorage = gridStorage;
            this.storageRepository = storageRepository;
        }

        public void Adicionar(string path, Documento documento)
        {
            var objectId = this.gridStorage.AdicionarOuAtualizar(path);

            var storage = Storage.New(
                documento,
                objectId.Pid);
            
            this.storageRepository.Salvar(storage);
        }
    }
}