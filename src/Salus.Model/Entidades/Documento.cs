using System;

namespace Salus.Model.Entidades
{
    public class Documento : Entidade
    {
        public virtual string Assunto { get; set; }

        public virtual DateTime DataCriacao { get; set; }

        public virtual long Tamanho { get; set; }
    }
}