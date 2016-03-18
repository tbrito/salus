namespace Salus.Model.Entidades
{
    public class AcessoDocumento : Entidade
    {
        public virtual Usuario Usuario { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}