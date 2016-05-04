namespace SalusCmd.Ecm6.Imports
{
    using Salus.Model.Entidades;
    using System;

    public class FileDto : Entidade
    {
        public string GroupDocCode
        {
            get;
            set;
        }

        public string Usr
        {
            get;
            set;
        }

        public string IndexUsr
        {
            get;
            set;
        }

        public DateTime? IndexDate
        {
            get;
            set;
        }

        public DateTime? Date
        {
            get;
            set;
        }

        public string Extension
        {
            get; 
            set;
        }
    }
}