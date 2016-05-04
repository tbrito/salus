namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using NHibernate;
    using NHibernate.Exceptions;
    using Salus.Model.Entidades;
    using Salus.Infra.Logs;
    using Salus.Model.Entidades.Import;
    using SharpArch.NHibernate;
    using Salus.Infra.Repositorios;
    using NHibernate.Util;

    public abstract class PaginatedImportBase<TEntity> where TEntity : Entidade
    {
        private const int ItemsPerPage = 1000;
        private readonly Dictionary<int, TEntity> itemsAlreadyImported;
        private IDictionary<int, int> depara;
        private TEntity entidadeImportada;
        private int count;
        
        protected PaginatedImportBase()
        {
            this.itemsAlreadyImported = new Dictionary<int, TEntity>();
        }

        public abstract string SqlEcm6
        {
            get;
        }

        public abstract List<TEntity> GetEntities(IStatelessSession session, int startRow, int endRow);

        public virtual void BeforeImport(int oldId, TEntity entity)
        {
        }

        public abstract void AfterImport(int oldId, TEntity entity, ISession session);

        public abstract bool ShouldImport(TEntity entity);

        public virtual Dictionary<int, TEntity> Execute(string message)
        {
            Log.App.InfoFormat(message);
            return this.InternalExecute();
        }

        protected bool CheckIfExist<TDepara>(Entidade entity) where TDepara : Ecm6ToEcm8
        {
            if (this.depara == null)
            {
                var deparas = NHibernateSession.Current
                    .CreateSQLQuery(this.SqlEcm6)
                    .SetResultTransformer(CustomResultTransformer<TDepara>.Do())
                    .List<TDepara>();

                this.depara = new Dictionary<int, int>();
                deparas.ForEach(x => this.depara.Add(x.Ecm6Id, x.Ecm8Id));
            }

            if (this.depara.ContainsKey(entity.Id) == false)
            {
                return true;
            }

            this.entidadeImportada = NHibernateSession
                .Current
                .Get<TEntity>(this.depara[entity.Id]);

            return false;
        }

        protected void ImportEntity(TEntity entity)
        {
            var oldId = entity.Id;

            if (this.ShouldImport(entity) == false)
            {
                Log.App.DebugFormat("{0} Já Importado. Id #{1}", entity.GetType().Name, entity.Id);

                if (this.itemsAlreadyImported.ContainsKey(oldId) == false)
                {
                    this.itemsAlreadyImported.Add(oldId, this.entidadeImportada);
                }
                
                return;
            }

            entity.Id = 0;

            this.BeforeImport(oldId, entity);
            NHibernateSession.Current.Save(entity);
            this.AfterImport(oldId, entity, NHibernateSession.Current);

            if (this.depara != null)
            {
                this.depara.Add(oldId, entity.Id);    
            }
            
            if (this.itemsAlreadyImported.ContainsKey(oldId) == false)
            {
                this.itemsAlreadyImported.Add(oldId, entity);
            }
            
            this.count++;
        }

        protected string GetPaginatedSql(string sql, int startRow, int endRow)
        {
            const string PaginatedSql = @"
select * from 
	({2}) as rowresult
where RowNum >= {0} and RowNum <= {1}
order by RowNum";

            return string.Format(PaginatedSql, startRow, endRow, sql);
        }

        private Dictionary<int, TEntity> InternalExecute(int startRow = 1, int endRow = ItemsPerPage)
        {
            var entities = new List<TEntity>();
            
            var currentStartRow = 0;
            var currentEndRow = 0;

            try
            {
                do
                {
                    currentStartRow = startRow;
                    currentEndRow = endRow;

                    ImportDatabase.Using(session =>
                    {
                        Log.App.InfoFormat("Obtendo do ecm6 de {0} a {1}", startRow, endRow);
                        entities = this.GetEntities(session, startRow, endRow);
                    });

                    startRow = endRow + 1;
                    endRow += ItemsPerPage;

                    foreach (var entity in entities)
                    {
                        this.ImportEntity(entity);
                    }
                }
                while (entities.Count > 0);
            }
            catch (GenericADOException exception)
            {
                Thread.Sleep(TimeSpan.FromMinutes(2).Milliseconds);
                this.InternalExecute(currentStartRow, currentEndRow);
            }

            return this.itemsAlreadyImported;
        }
    }
}