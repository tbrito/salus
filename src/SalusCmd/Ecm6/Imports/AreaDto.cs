namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using Framework.Modelo;

    public class AreaDto : Entidade
    {
        public string Descricao
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }

        public string Restricted
        {
            get; 
            set;
        }

        public string Abrev
        {
            get;
            set;
        }
    }
}