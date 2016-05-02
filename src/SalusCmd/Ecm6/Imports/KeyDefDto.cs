namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;

    public class KeyDefDto : Entidade
    {
        public string GroupDocCode
        {
            get;
            set;
        }

        public string Obrig
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public int TipoDadoId
        {
            get;
            set;
        }
    }
}