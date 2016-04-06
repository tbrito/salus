using Salus.Model.Entidades;
using System.Runtime.Serialization;

namespace Salus.Model.UI
{
    [KnownType(typeof(TipoDocumento))]
    public class TipoDocumentoViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool EhPasta { get; set; }
        public string Nome { get; set; }
        public int ParentId { get; set; }
        public TipoDocumento Parent { get; set; }
    }
}
