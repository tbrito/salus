namespace Salus.Model.Servicos
{
    public class StorageServico
    {
        private IStorage storage;

        public void Adicionar(string path, int id)
        {
            this.storage.AdicionarOuAtualizar(path);
        }
    }
}