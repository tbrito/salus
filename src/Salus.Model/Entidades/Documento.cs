using System;
using System.Collections.Generic;

namespace Salus.Model.Entidades
{
    public class Documento : Entidade
    {
        public virtual string Assunto { get; set; }

        public virtual DateTime? DataCriacao { get; set; }

        public virtual long Tamanho { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }

        public virtual string CpfCnpj { get; set; }

        public virtual SearchStatus SearchStatus { get; set; }

        public virtual IList<Indexacao> Indexacao { get; set; }

        public virtual bool EhPreIndexacao { get; set; }

        public virtual bool EhIndice { get; set; }

        public virtual bool Bloqueado { get; set; }
    }
}