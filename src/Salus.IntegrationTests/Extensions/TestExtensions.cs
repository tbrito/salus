using Salus.Model.Entidades;
using SharpArch.NHibernate;

namespace Salus.IntegrationTests.Extensions
{
    public static class TestExtensions
    {
        public static T Persistir<T>(this T entidade) where T : Entidade
        {
            NHibernateSession.Current.SaveOrUpdate(entidade);
            NHibernateSession.Current.Flush();

            return entidade;
        }
    }
}
