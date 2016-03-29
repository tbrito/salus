using System;

namespace Salus.Model.Entidades
{
    public class Trilha : Entidade
    {
        public virtual DateTime Data { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Recurso { get; set; }
        public virtual TipoTrilha Tipo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
