using System;
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;

namespace Salus.Data
{
    public class SessaoDoBanco
    {
        public ISession CurrentSession
        {
            get
            {
                if (HttpContext.Current.Items["Acesso"] == null)
                {
                    HttpContext.Current.Items["Acesso"] = this.Iniciar();
                }

                return (ISession)HttpContext.Current.Items["Acesso"];
            }
        }

        private ISession Iniciar()
        {
            var mappings = GetMappingsAssemblies();

            var fluentConfiguration = Fluently.Configure()
                .Database(BancoDeDados.Configuration())
                .Mappings(mappings)
                .BuildConfiguration();

            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            var session = sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;

            return session;
        }

        private Action<MappingConfiguration> GetMappingsAssemblies()
        {
            Action<MappingConfiguration> mappings = x =>
            {
                var mapping = x.FluentMappings;

                foreach (var mapeamento in BancoDeDados.ObterMapeamentos())
                {
                    mapping.AddFromAssembly(mapeamento);
                }
            };

            return mappings;
        }

        public void Dispose()
        {
            this.CurrentSession.Close();
            this.CurrentSession.Dispose();
        }
    }
}