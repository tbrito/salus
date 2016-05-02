namespace SalusCmd.Ecm6
{
    using Salus.Infra.ConnectionInfra;
    using SharpArch.NHibernate;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DapperHelper
    {
        private static IDbConnection ecm6Connection;

        public static void UsingConnection(Action<IDbConnection> action)
        {
            action(NHibernateSession.Current.Connection);
        }
        
        public static void UsingEcm6Connection(Action<IDbConnection> action)
        {
            ecm6Connection = new SqlConnection(BancoDeDados.ObterConnectionStringEcmAntigo());

            action(ecm6Connection);
        }
    }
}