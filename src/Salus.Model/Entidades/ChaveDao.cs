using System;

namespace Salus.Model.Entidades
{
    public class ChaveDao : Entidade
    {
        public virtual string ItensLista { get; set;  }

        public virtual string Mascara { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool Obrigatorio { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual int TipoDado { get; set; }

        public virtual int TipoDocumento { get; set; }
    }
}