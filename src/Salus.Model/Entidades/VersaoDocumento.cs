using System;

namespace Salus.Model.Entidades
{
    public class VersaoDocumento : Entidade
    {
        public virtual Documento Documento { get; set; }

        public virtual DateTime? CriadoEm { get; set; }

        public virtual Usuario CriadoPor { get; set; }

        public virtual string Comentario { get; set; }

        public virtual int Versao { get; set; }
    }
}