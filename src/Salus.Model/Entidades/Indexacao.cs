using Salus.Model.Entidades;

namespace Salus.Model
{
    public class Indexacao : Entidade
    {
        public virtual string Valor { get; set; }

        public virtual int CampoId { get; set; }

        public virtual int DocumentoId { get; set; }
    }
}