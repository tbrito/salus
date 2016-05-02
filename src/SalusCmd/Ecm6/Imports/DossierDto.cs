namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using Framework.Modelo;

    public class DossierDto : Entidade
    {
        public DateTime? Date
        {
            get;
            set;
        }

        public int GroupDoc
        {
            get;
            set;
        }
    }
}