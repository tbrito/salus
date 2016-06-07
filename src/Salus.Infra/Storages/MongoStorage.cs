namespace Salus.Infra.Storages
{
    using System.IO;
    using Salus.Model;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB.Driver.GridFS;
    using System.Configuration;
    using System;
    using Logs;
    public class MongoStorage : IMongoStorage
    {
        public GridFSBucket gridFs;

        public MongoStorage()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["Storage.ConnectionString"]);
            var database = client.GetDatabase("salusdb");
            this.gridFs = new GridFSBucket(database);
        }

        public string AdicionarOuAtualizar(string filename)
        {
            Stream valor = File.Open(filename, FileMode.Open);
            var fileInfo = gridFs.UploadFromStream(filename, valor);
            
            valor.Close();
            
            return fileInfo.ToString();
        }

        public void Apagar(string objectId)
        {
            this.gridFs.Delete(ObjectId.Parse(objectId));
        }
        
        public string Obter(string objectId, string tipoArquivo)
        {
            var diretorioConteudo = Path.Combine(Aplicacao.Caminho, "storage-temp", objectId);

            Directory.CreateDirectory(diretorioConteudo);

            var fileFullPath = Path.Combine(
                diretorioConteudo,
                objectId + "." + tipoArquivo);

            var inicioMongo = DateTime.Now;
            var streamToDownloadTo = new FileStream(fileFullPath, FileMode.Create);
            this.gridFs.DownloadToStream(ObjectId.Parse(objectId), streamToDownloadTo);

            streamToDownloadTo.Close();
            var fimMongo = DateTime.Now;

            Log.App.InfoFormat("Documento foi localizado em {0} ms", fimMongo.Subtract(inicioMongo).Milliseconds);

            return fileFullPath;
        }
    }
}
