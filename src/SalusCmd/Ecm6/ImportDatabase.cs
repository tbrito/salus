namespace SalusCmd.Ecm6
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Reflection;
    using NHibernate;

    public static class ImportDatabase
    {
        private static ISessionFactory sessionFactorySingleton;

        public static void Using(Action<IStatelessSession> action)
        {
            using (var sessionFactory = GetSessionFactory())
            {
                using (var session = sessionFactory.OpenStatelessSession())
                {
                    using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        action(session);
                        transaction.Commit();
                    }
                }
            }
        }

        public static ISessionFactory GetSessionFactory()
        {
            if (sessionFactorySingleton == null)
            {
                var connectionString = GetConnectionString(ConfigurationManager.AppSettings["Import.ConnectionString"]);

                var properties = new Dictionary<string, string>
                {
                    { "connection.driver_class", "NHibernate.Driver.SqlClientDriver" },
                    { "dialect", "NHibernate.Dialect.MsSql2005Dialect" },
                    { "connection.provider", "NHibernate.Connection.DriverConnectionProvider" },
                    { "connection.connection_string", connectionString },
                };
                
                var configuration = new NHibernate.Cfg.Configuration();
                configuration.AddProperties(properties);
                configuration.AddAssembly(Assembly.GetExecutingAssembly());

                sessionFactorySingleton = configuration.BuildSessionFactory();
            }

            return sessionFactorySingleton;
        }

        private static string GetConnectionString(string connectionString)
        {
            return connectionString;
        }
    }
}
