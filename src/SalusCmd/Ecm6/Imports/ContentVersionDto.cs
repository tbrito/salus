namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using Framework.Modelo;

    public class ContentVersionDto : Entidade
    {
        public DateTime? Data
        {
            get;
            set;
        }

        public string UsrCode
        {
            get;
            set;
        }

        public string Obs
        {
            get;
            set;
        }

        public string DocCode
        {
            get;
            set;
        }

        public string Revisao
        {
            get;
            set;
        }
    }
}