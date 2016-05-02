namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using Framework.Modelo;

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
    }
}