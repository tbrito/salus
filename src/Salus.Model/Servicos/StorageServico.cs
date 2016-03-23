using System;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using MongoDB.Bson;

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
                objectId.);
            
            this.storageRepository.Salvar(storage);
        }

        public string Obter(int documentoId)
        {
            var storage = this.storageRepository.ObterPorDocumentoId(documentoId);
            var stream = this.gridStorage.Obter(ObjectId.Parse(storage.MongoId.ToString()));

            return string.Empty;
        }
    }
}