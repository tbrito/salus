using FluentNHibernate.Cfg;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.Logs;
using Salus.Infra.Util;
using SharpArch.NHibernate;
using System;
using System.IO;

namespace Salus.Infra.DataAccess
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        public bool EstaAberta
        {
            get
            {
                return NHibernateSession.Current.IsOpen;
            }
        }

        public void Dispose()
        {
            try
            {
                if (this.ThereIsActiveTransaction())
                {
                    this.Commit();
                }
            }
            catch
            {
                this.RollBack();
                throw;
            }
            finally
            {
                Log.App.DebugFormat(
                    "Session {0}: disposing",
                    NHibernateSession.Current.GetHashCode());
            }
        }

        public static void Boot()
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
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

        public IUnidadeDeTrabalho Iniciar()
        {
            NHibernateSession.Current.BeginTransaction();

            return this;
        }

        public void Commit()
        {
            if (this.ThereIsActiveTransaction() == false)
            {
                throw new InvalidOperationException("There is no transaction in progress. Call Begin() before call Commit()");
            }

            if (NHibernateSession.Current.Transaction.WasCommitted)
            {
                return;
            }

            NHibernateSession.Current.Transaction.Commit();

            Log.App.DebugFormat(
                "Session {0}: transaction committed {1}",
                NHibernateSession.Current.GetHashCode(),
                NHibernateSession.Current.Transaction.GetHashCode());
        }

        public void RollBack()
        {
            if (NHibernateSession.Current == null || NHibernateSession.Current.Transaction == null)
            {
                return;
            }

            if (NHibernateSession.Current.Transaction.IsActive)
            {
                NHibernateSession.Current.Transaction.Rollback();

                Log.App.DebugFormat(
                    "Session {0}: transaction rolledback {1}",
                    NHibernateSession.Current.GetHashCode(),
                    NHibernateSession.Current.Transaction.GetHashCode());
            }
        }

        private bool ThereIsActiveTransaction()
        {
            if (NHibernateSession.Current != null && NHibernateSession.Current.Transaction != null)
            {
                return NHibernateSession.Current.Transaction.IsActive;
            }

            return false;
        }
    }
}