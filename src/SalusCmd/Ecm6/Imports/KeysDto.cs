using Salus.Model.Entidades;

namespace SalusCmd.Ecm6.Imports
{
    public class KeysDto : Entidade
    {
        public string DocCode
        {
            get;
            set;
        }

        public string KeyDefCode
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public long RowNum
        {
            get;
            set;
        }
    }
}