namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.IO;
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

            var extensao = Path.GetExtension(path);

            var storage = Storage.New(
                documento,
                objectId,
                extensao.Replace(".", string.Empty));
            
            this.storageRepository.Salvar(storage);
        }

        public string Obter(int documentoId)
        {
            var storage = this.storageRepository.ObterPorDocumentoId(documentoId);
            var stream = this.gridStorage.Obter(storage.MongoId, storage.FileType);

            return string.Empty;
        }
    }
}