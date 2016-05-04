namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;
    using System;

    public class InflowDto : Entidade
    {
        public string DocCode
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public string FromUsr
        {
            get;
            set;
        }

        public DateTime? Date1
        {
            get;
            set;
        }

        public string Usr
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