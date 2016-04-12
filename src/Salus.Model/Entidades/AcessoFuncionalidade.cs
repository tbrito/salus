namespace Salus.Model.Entidades
{
    public class AcessoFuncionalidade : Entidade
    {
        public Papel Papel { get; set; }

        /// <summary>
        /// Codigo da Area do perfil ou do usuario
        /// </summary>
        public int AtorId { get; set; }

        public Funcionalidade Funcionalidade { get; set; }
    }
}