namespace SalusCmd.Ecm6
{
    using FluentNHibernate.Cfg;
    using Salus.Infra.ConnectionInfra;
    using Salus.Infra.DataAccess;
    using Salus.Infra.Migrations;
    using Salus.Infra.Util;
    using SharpArch.NHibernate;
    using System;
    using System.Configuration;
    using System.IO;

    public class SchemaUpdateTask : ITarefa
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Atualiza o schema de banco de dados";
            }
        }

        public string Comando
        {
            get
            {
                return "schema update";
            }
        }

        public void Executar(params string[] args)
        {
            string[] mappings = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salus.Infra.dll")
            };

            NHibernateInitializer.Instance().InitializeNHibernateOnce(() =>
            {
                NHibernateSession.Init(
                    new ThreadSessionStorage(),
                    mappings,
                    null, null, null, null, BancoDeDados.Configuration());
            });

            var fluentConfiguration = Fluently.Configure()
               .Database(BancoDeDados.Configuration())
               .Mappings(m =>
               {
                   m.FluentMappings.Conventions.Add<EnumConvention>();
                   m.FluentMappings.Conventions.Add<EnumerationTypeConvention>();
               });


            var migrator = new Migrator(
              ConfigurationManager.AppSettings["Database.ConnectionString"]);

            migrator.Migrate(runner => runner.MigrateUp());
        }
    }
}
