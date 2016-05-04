using Salus.Model.Entidades;

namespace SalusCmd.Ecm6.Imports
{
    public class AreaDto : Entidade
    {
        public string Descricao
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }

        public string Restricted
        {
            get; 
            set;
        }

        public string Abrev
        {
            get;
            set;
        }
    }
}