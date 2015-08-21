namespace Salus.Data
{
    using NHibernate;

    public class DatabaseSession : IDatabaseSession
    {
        private readonly ISessaoHibernate sessaoHibernate;

        public DatabaseSession(ISessaoHibernate sessaoHibernate)
        {
            this.sessaoHibernate = sessaoHibernate;
        }

        public void Iniciar()
        {
            this.Session = this.sessaoHibernate.Criar();
        }

        public ISession Session {
            get
            {
                return this.sessaoHibernate.ObterSessao();
            }
            set
            {
                this.sessaoHibernate.SetarSessao(value);
            }
        }

        public void Dispose()
        {
            this.sessaoHibernate.Encerrar();
        }
    }
}
