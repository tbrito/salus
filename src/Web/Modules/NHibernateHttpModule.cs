using FluentNHibernate.Cfg;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.DataAccess;
using Salus.Infra.Logs;
using Salus.Infra.Util;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.IO;
using System.Web;

namespace Web.Modules
{
    public class NHibernateHttpModule : IHttpModule
    {
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.BeginRequest;
            context.EndRequest += this.EndRequest;
        }

        private void EndRequest(object sender, EventArgs e)
        {
            var transaction = NHibernateSession.Current.BeginTransaction();

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
            }
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Salus.Infra.dll")
            };

            NHibernateInitializer.Instance().InitializeNHibernateOnce(() =>
            {
                NHibernateSession.Init(
                    new ThreadSessionStorage(),
                    mappings,
                    null, null, null, null, BancoDeDados.Configuration());
            });

            var fluentConfiguration = Fluently.Configure()
               .Database(BancoDeDados.Configuration())
               .Mappings(m =>
               {
                   m.FluentMappings.Conventions.Add<EnumConvention>();
                   m.FluentMappings.Conventions.Add<EnumerationTypeConvention>();
               });
        }
    }
}