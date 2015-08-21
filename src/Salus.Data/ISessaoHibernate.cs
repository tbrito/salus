using NHibernate;

namespace Salus.Data
{
    public interface ISessaoHibernate
    {
        ISession Criar();

        void SetarSessao(ISession value);
        
        ISession ObterSessao();
        
        void Encerrar();
    }
}