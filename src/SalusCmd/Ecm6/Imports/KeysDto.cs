namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;

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