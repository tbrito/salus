namespace Salus.Model.Entidades.Import
{
    public abstract class Ecm6ToEcm8 : Entidade
    {
        public virtual int Ecm6Id
        {
            get;
            set;
        }

        public virtual int Ecm8Id
        {
            get;
            set;
        }
    }
}