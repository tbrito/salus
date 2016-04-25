using FluentNHibernate.Cfg;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.Util;
using Salus.Model.Entidades;
using SharpArch.NHibernate;
using System;
using System.IO;

namespace Salus.Infra.Seeds
{
    public static class RepositorioExtensions
    {
        public static T Persistir<T>(this T entidade) where T : Entidade
        {
            ResetarConexao();
            NHibernateSession.Current.SaveOrUpdate(entidade);
            NHibernateSession.Current.Flush();

            return entidade;
        }

        public static void ResetarConexao()
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
