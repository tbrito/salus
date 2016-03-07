namespace Salus.Model
{
    using MongoDB.Bson;
    using System.IO;

    public interface IStorage
    {
        ObjectId AdicionarOuAtualizar(string fileName);

        Stream Obter(ObjectId chave);

        void Apagar(ObjectId chave);
    }
}
