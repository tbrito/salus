namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;
    using System;

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