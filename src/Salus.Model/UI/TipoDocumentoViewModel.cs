namespace Salus.Model.UI
{
    public class TipoDocumentoViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool EhPasta { get; set; }
        public string Nome { get; set; }
        public int ParentId { get; set; }
    }
}
