using NHibernate;

namespace Salus.Data
{
    public interface IDatabaseSession
    {
        ISession Session { get; set; }
        
        void Iniciar();

        void Dispose();
    }
}