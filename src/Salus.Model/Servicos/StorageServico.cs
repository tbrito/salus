namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System;
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

        public void Adicionar(string path, string salusId)
        {
            var objectId = this.gridStorage.AdicionarOuAtualizar(path);

            var extensao = Path.GetExtension(path);

            var storage = Storage.New(
                salusId,
                objectId,
                extensao.Replace(".", string.Empty));
            
            this.storageRepository.Salvar(storage);
        }

        public string Obter(string salusId)
        {
            var storage = this.storageRepository.ObterPorSalusId(salusId);

            if (storage == null)
            {
                return string.Empty;
            }

            var arquivo = this.ArquivoPdfExisteEmCache(storage);

            if (File.Exists(arquivo) == false)
            {
                arquivo = this.gridStorage.Obter(storage.MongoId, storage.FileType);
            }

            return arquivo;
        }

        private string ArquivoPdfExisteEmCache(Storage storage)
        {
            var diretorioConteudo = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "storage-temp",
                            storage.MongoId);

            Directory.CreateDirectory(diretorioConteudo);

            var fileFullPath = Path.Combine(
                diretorioConteudo,
                storage.MongoId + ".pdf");

            return fileFullPath;
        }
    }
}