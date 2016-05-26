namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;

    public class UserDto : Entidade
    {
        public string Name
        {
            get;
            set;
        }

        public string Active
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string Deleted
        {
            get;
            set;
        }

        public string Profile
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }
    }
}