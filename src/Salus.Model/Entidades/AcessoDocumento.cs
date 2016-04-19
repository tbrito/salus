namespace Salus.Model.Entidades
{
    public class AcessoDocumento : Entidade
    {
        public virtual Papel Papel { get; set; }

        /// <summary>
        /// Codigo da Area do perfil ou do usuario
        /// </summary>
        public virtual int AtorId { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}