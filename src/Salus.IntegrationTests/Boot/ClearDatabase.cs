using System;
using Salus.Infra.ForTests;
using SharpArch.NHibernate;
using System.IO;
using Salus.Infra.ConnectionInfra;

namespace Salus.IntegrationTests.Boot
{
    public class ClearDatabase : IClearDatabase
    {
        public void Execute()
        {
            this.IniciarBanco();

            NHibernateSession.Current.Delete("from Documento");
            NHibernateSession.Current.Delete("from TipoDocumento");
            NHibernateSession.Current.Delete("from Area");
            NHibernateSession.Current.Delete("from Perfil");
            NHibernateSession.Current.Delete("from Usuario");
            NHibernateSession.Current.Delete("from Storage");
            NHibernateSession.Current.Delete("from Chave");
            NHibernateSession.Current.Delete("from Indexacao");
            NHibernateSession.Current.Delete("from Trilha");
            NHibernateSession.Current.Delete("from Workflow");

            NHibernateSession.Current.Flush();
            NHibernateSession.Current.Close();
        }

        private void IniciarBanco()
        {
            string[] mappings = new string[]
          {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
          };

            NHibernateSession.Reset();

            NHibernateSession.Init(
                new SimpleSessionStorage(),
                mappings,
                null, null, null, null, BancoDeDados.Configuration());
        }
    }
}
