using System;
using System.Collections;
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;

namespace Salus.Data
{
    public class SessaoHibernate : ISessaoHibernate
    {
        private ISession session;

        public ISession Criar()
        {
            this.session = this.CreateSessionFactory().OpenSession();
            this.session.FlushMode = FlushMode.Commit;
            this.session.Transaction.Begin();

            return this.session;
        }

        private ISession ObterSessaoWeb()
        {
            if (HttpContext.Current.Items["Acesso"] == null)
            {
                HttpContext.Current.Items["Acesso"] = this.Criar();
            }

            return (ISession)HttpContext.Current.Items["Acesso"];
        }

        private ISession ObterSessaoLocal()
        {
            if (ContextoLocal.Items == null)
            {
                ContextoLocal.Items = new Hashtable();
            }

            if (ContextoLocal.Items["Acesso"] == null)
            {
                ContextoLocal.Items["Acesso"] = this.Criar();
            }

            return (ISession)ContextoLocal.Items["Acesso"];
        }

        public void SetarSessao(ISession value)
        {
            if (this.EhWeb)
            {

                HttpContext.Current.Items["Acesso"] = value;
            }
            else
            {
                ContextoLocal.Items.Add("Acesso", value);
            }
        }

        public ISession ObterSessao()
        {
            return this.EhWeb ?
                   this.ObterSessaoWeb() :
                   this.ObterSessaoLocal();
        }

        public void Encerrar()
        {
            if (this.session == null)
            {
                return;
            }

            try
            {
                if (this.session.IsOpen)
                {
                    if (this.session.Transaction.IsActive)
                    {
                        this.session.Transaction.Commit();
                    }

                    this.session.Close();
                    this.session.Dispose();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Erro: " + exception);
            }
            finally
            {
                this.session.Dispose();
                this.session = null;
            }
        }

        private bool EhWeb
        {
            get
            {
                return HttpContext.Current != null;
            }
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                    .Database(BancoDeDados.Configuration)
                    .Mappings(x => x.FluentMappings.AddFromAssembly(BancoDeDados.mapeamentos[0]))
                    .BuildSessionFactory();
        }
    }
}