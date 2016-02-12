using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using NHibernate;
using SharpArch.NHibernate;

namespace Salus.Infra.ConnectionInfra
{
    public abstract class BancoDeDados
    {
        public static IList<Assembly> mapeamentos = new List<Assembly>();

        public ISession Sessao
        {
            get
            {
                return NHibernateSession.Current;
            }
        }
        public static string ObterConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static IPersistenceConfigurer Configuration()
        {
            return MsSqlConfiguration
                .MsSql2012
                .ConnectionString(ObterConnectionString());
        }

        public static void AdicionarMapeamento(Assembly assembly)
        {
            mapeamentos.Add(assembly);
        }

        public static IEnumerable<Assembly> ObterMapeamentos()
        {
            return mapeamentos;
        }
    }
}