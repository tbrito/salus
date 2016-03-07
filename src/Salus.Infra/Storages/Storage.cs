namespace Salus.Infra.Storages
{
    using System.IO;
    using Salus.Model;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB.Driver.GridFS;

    public class Storage : IStorage
    {
        public GridFSBucket gridFs;

        public Storage()
        {
            var client = new MongoClient("mongodb://localhost:21021");
            var database = client.GetDatabase("testdb");
            this.gridFs = new GridFSBucket(database);
        }

        public ObjectId AdicionarOuAtualizar(string filename)
        {
            Stream valor = null;
            var fileInfo = gridFs.UploadFromStream(filename, valor);

            return fileInfo;

        }

        public void Apagar(ObjectId objectId)
        {
            this.gridFs.Delete(objectId);
        }

        public Stream Obter(ObjectId objectId)
        {
            Stream valor = null;
            this.gridFs.DownloadToStream(objectId, valor);

            return valor;
        }
    }
}
