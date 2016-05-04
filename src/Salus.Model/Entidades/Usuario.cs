using System;

namespace Salus.Model.Entidades
{
    public class Usuario : Entidade
    {
        public virtual string Email { get; set; }

        public virtual string Login { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Senha { get; set; }

        public virtual bool Expira { get; set; }

        public virtual DateTime? ExpiraEm { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual string MotivoInatividade { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual Area Area { get; set; }

        public virtual string Avatar { get; set; }
    }
}
