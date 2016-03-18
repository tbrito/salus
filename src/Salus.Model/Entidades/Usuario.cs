namespace Salus.Model.Entidades
{
    public class Usuario : Entidade
    {
        public virtual string Email { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Senha { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual Area Area { get; set; }
    }
}
