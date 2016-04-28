using FluentNHibernate.Cfg;
using Salus.Infra.ConnectionInfra;
using Salus.Infra.DataAccess;
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
            NHibernateSession.Current.SaveOrUpdate(entidade);
            NHibernateSession.Current.Flush();

            return entidade;
        }
    }
}
