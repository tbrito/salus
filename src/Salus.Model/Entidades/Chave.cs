using Salus.Model.Entidades;

namespace Salus.Model
{
    public class Chave : Entidade
    {
        public virtual string[] Lista { get; set; }

        public virtual string Mascara { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool Obrigatorio { get; set; }

        public virtual TipoDado TipoDado { get; set; }
    }
}