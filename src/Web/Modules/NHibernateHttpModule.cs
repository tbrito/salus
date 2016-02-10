using FluentNHibernate.Cfg;
using NHibernate.AspNet.Identity.Helpers;
using Salus.Infra.ConnectionInfra;
using Salus.Model.Entidades;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.Web;

namespace Web.Modules
{
    public class NHibernateHttpModule : IHttpModule
    {
        private HttpApplication context;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            this.context = context;
            context.BeginRequest += this.BeginRequest;
            context.EndRequest += this.EndRequest;
        }

        private void EndRequest(object sender, EventArgs e)
        {
            NHibernateSession.Current.Close();
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            var internalTypes = new[] {
                typeof(Usuario)
            };

            var mapping = MappingHelper.GetIdentityMappings(internalTypes);

            string[] mappings = new string[mapping.Items.Length];

            for (int i = 0; i < mapping.Items.Length; i++)
            {
                mappings[i] = mapping.Items[i].ToString();
            }
            
            NHibernateSession.Init(
                new WebSessionStorage(this.context), 
                mappings, 
                null, null, null, null, BancoDeDados.Configuration());
        }
    }
}