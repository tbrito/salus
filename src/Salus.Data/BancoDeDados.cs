using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg.Db;

namespace Salus.Data
{
    public class BancoDeDados
    {
        public static IList<Assembly> mapeamentos = new List<Assembly>();

        public static string ObterDialect()
        {
            return "NHibernate.Dialect.PostgreSQLDialect";
        }

        public static string ObterProvider()
        {
            return "NHibernate.Connection.DriverConnectionProvider";
        }

        public static string ObterConnectionString()
        {
            return ImaculadaConnectionString.GetConnectionString();
        }

        public static string ObterDriver()
        {
            return "NHibernate.Driver.NpgsqlDriver";
        }

        public static IPersistenceConfigurer Configuration()
        {
            return PostgreSQLConfiguration
                .Standard
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