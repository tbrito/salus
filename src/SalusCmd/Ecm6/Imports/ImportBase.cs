namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Salus.Model.Entidades;
    using Salus.Infra.Logs;
    using SharpArch.NHibernate;
    using Salus.Model.Entidades.Import;
    using Salus.Infra.Repositorios;
    public abstract class ImportBase<TEntity> 
        where TEntity : Entidade
    {
        protected Dictionary<int, TEntity> itemsForReference;
        protected TEntity entidadeImportada;
        protected int count;
        
        protected ImportBase()
        {
            this.itemsForReference = new Dictionary<int, TEntity>();
        }

        public abstract string SqlEcm6
        {
            get;
        }

        public abstract IList<TEntity> GetEntities(IStatelessSession session);
        
        public virtual void BeforeImport(int oldId, TEntity entity)
        {
        }

        public virtual void AfterImport(int oldId, TEntity entity, ISession session)
        {
        }

        public abstract bool ShouldImport(TEntity entity);

        public virtual Dictionary<int, TEntity> Execute(string message)
        {
            Log.App.InfoFormat(message);

            IList<TEntity> entities = new List<TEntity>();

            Log.App.DebugFormat("Obtendo registros no ecm 6");

            ImportDatabase.Using(session =>
            {
                entities = this.GetEntities(session).ToList();
            });

            Log.App.DebugFormat("Encontrado {0} registros", entities.Count);
            Log.App.DebugFormat("Importando registros no ecm 8...");

            foreach (var entity in entities)
            {
                this.ImportEntity(entity);
            }

            return this.itemsForReference;         
        }

        protected bool CheckIfExist<TDepara>(Entidade entity) where TDepara : Ecm6ToEcm8
        {
            var depara = NHibernateSession.Current
                .CreateSQLQuery(this.SqlEcm6)
                .SetInt32("ecm6Id", entity.Id)
                .SetResultTransformer(CustomResultTransformer<TDepara>.Do())
                .SetTimeout(0)
                .UniqueResult<TDepara>();

            if (depara == null)
            {
                return true;
            }

            this.entidadeImportada = NHibernateSession.Current.Get<TEntity>(depara.Ecm8Id);

            return false;
        }

        protected void ImportEntity(TEntity entity)
        {
            var oldId = entity.Id;

            if (this.ShouldImport(entity) == false)
            {
                Log.App.DebugFormat("{0} Já Importado. Id #{1}", entity.GetType().Name, entity.Id);
                this.itemsForReference.Add(oldId, this.entidadeImportada);
                return;
            }

            entity.Id = 0;

            this.BeforeImport(oldId, entity);
            NHibernateSession.Current.Save(entity);
            this.AfterImport(oldId, entity, NHibernateSession.Current);
            this.itemsForReference.Add(oldId, entity);
            this.count++;
        }
    }
}