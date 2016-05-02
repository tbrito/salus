namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;

    public class GroupDocDto : Entidade
    {
        public string Code
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public string Active
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }

        public string Folder
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