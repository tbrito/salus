namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using Framework.Modelo;

    public class KeysDto : Entidade
    {
        public string DocCode
        {
            get;
            set;
        }

        public string KeyDefCode
        {
            get;
            set;
        }

        public string Descricao
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