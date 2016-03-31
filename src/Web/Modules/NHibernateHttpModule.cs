using Salus.Infra.ConnectionInfra;
using Salus.Infra.Logs;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.IO;
using System.Web;

namespace Web.Modules
{
    public class NHibernateHttpModule : IHttpModule
    {
        private WebSessionStorage webSessionStorage;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            this.webSessionStorage = new WebSessionStorage(context);
            context.BeginRequest += this.BeginRequest;
            context.EndRequest += this.EndRequest;
        }

        private void EndRequest(object sender, EventArgs e)
        {
            var transaction = NHibernateSession.Current.Transaction;

            try
            {
                transaction.Commit();
            }
            catch (Exception exception)
            {
                Log.App.Error("erro ao atualizar", exception);
                if (transaction.IsActive)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Salus.Infra.dll")
            };

            NHibernateInitializer.Instance().InitializeNHibernateOnce(() => 
                NHibernateSession.Init(
                    this.webSessionStorage,
                    mappings,
                    null, null, null, null, BancoDeDados.Configuration()));
        }
    }
}