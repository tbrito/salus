using Salus.Model.Entidades;
using System;
using System.Runtime.Serialization;

namespace Salus.Model.UI
{
    [KnownType(typeof(Documento))]
    [KnownType(typeof(Usuario))]
    [KnownType(typeof(Area))]
    public class WorkflowViewModel
    {
        public int Id { get; set; }
        public virtual DateTime CriadoEm { get; set; }
        public virtual DateTime? FinalizadoEm { get; set; }
        public virtual dynamic De { get; set; }
        public virtual dynamic Documento { get; set; }
        public virtual string Mensagem { get; set; }
        public virtual dynamic Para { get; set; }
        public virtual WorkflowStatus Status { get; set; }
        public virtual bool Lido { get; set; }
    }
}