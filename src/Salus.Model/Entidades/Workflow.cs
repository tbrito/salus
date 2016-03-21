using System;

namespace Salus.Model.Entidades
{
    public class Workflow : Entidade
    {
        public virtual DateTime CriadoEm { get; set; }
        public virtual DateTime? FinalizadoEm { get; set; }
        public virtual Usuario De { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual string Mensagem { get; set; }
        public virtual Usuario Para { get; set; }
        public virtual WorkflowStatus Status { get; set; }
        public virtual bool Lido { get; set; }

        public static Workflow Novo(Documento documento, Usuario usuarioAtual)
        {
            return new Workflow
            {
                Documento = documento,
                Status = 0,
                Para = usuarioAtual,
                De = usuarioAtual,
                Mensagem = "Documento Importado",
                CriadoEm = DateTime.Now,
                FinalizadoEm = null,
                Lido = false
            };
        }
    }
}