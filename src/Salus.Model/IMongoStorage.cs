namespace Salus.Model
{
    using MongoDB.Bson;
    using System.IO;

    public interface IMongoStorage
    {
        string AdicionarOuAtualizar(string fileName);

        string Obter(string chave, string tipoArquivo);

        void Apagar(string chave);
    }
}
