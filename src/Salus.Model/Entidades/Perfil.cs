namespace Salus.Model.Entidades
{
    public class Perfil : Entidade
    {
        public virtual bool Ativo { get; set; }

        public virtual string Nome { get; set; }
    }
}
