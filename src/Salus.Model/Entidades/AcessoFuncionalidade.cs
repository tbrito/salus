namespace Salus.Model.Entidades
{
    public class AcessoFuncionalidade : Entidade
    {
        public virtual Papel Papel { get; set; }

        /// <summary>
        /// Codigo da Area do perfil ou do usuario
        /// </summary>
        public virtual int AtorId { get; set; }

        public virtual Funcionalidade Funcionalidade { get; set; }
    }
}