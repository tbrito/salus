namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using Veros.Framework.Modelo;
    using Veros.Ecm.Model.Enums;

    public class DossierInsertDto : Entidade
    {
        public DateTime? CreatedAt
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public ContentType ContentType
        {
            get;
            set;
        }
    }
}