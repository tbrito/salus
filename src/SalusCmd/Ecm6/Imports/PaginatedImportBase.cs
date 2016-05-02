namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Model.Entities.Import;
    using NHibernate;
    using NHibernate.Exceptions;
    using Veros.Data;
    using Veros.Data.Hibernate;
    using Veros.Framework;
    using Veros.Framework.Modelo;
    using Veros.Framework.Performance;

    public abstract class PaginatedImportBase<TEntity> where TEntity : Entidade
    {
        private const int ItemsPerPage = 1000;
        private readonly IUnitOfWork unitOfWork;
        private readonly Dictionary<int, TEntity> itemsAlreadyImported;
        private IDictionary<int, int> depara;
        private TEntity entidadeImportada;
        private int count;
        
        protected PaginatedImportBase()
        {
            this.unitOfWork = IoC.Current.Resolve<IUnitOfWork>();
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
            Log.Application.InfoFormat(message);
            return this.InternalExecute();
        }

        protected bool CheckIfExist<TDepara>(Entidade entity) where TDepara : Ecm6ToEcm8
        {
            if (this.depara == null)
            {
                var deparas = this.unitOfWork.Current.CurrentSession
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

            this.entidadeImportada = this.unitOfWork
                .Current
                .CurrentSession
                .Get<TEntity>(this.depara[entity.Id]);

            return false;
        }

        protected void ImportEntity(TEntity entity)
        {
            var oldId = entity.Id;

            if (this.ShouldImport(entity) == false)
            {
                Log.Application.DebugFormat("{0} Já Importado. Id #{1}", entity.GetType().Name, entity.Id);

                if (this.itemsAlreadyImported.ContainsKey(oldId) == false)
                {
                    this.itemsAlreadyImported.Add(oldId, this.entidadeImportada);
                }
                
                return;
            }

            entity.Id = 0;

            this.BeforeImport(oldId, entity);
            this.unitOfWork.Current.CurrentSession.Save(entity);
            this.AfterImport(oldId, entity, this.unitOfWork.Current.CurrentSession);

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
            var timer = new Medicao();
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
                        Log.Application.InfoFormat("Obtendo do ecm6 de {0} a {1}", startRow, endRow);
                        entities = this.GetEntities(session, startRow, endRow);
                    });

                    startRow = endRow + 1;
                    endRow += ItemsPerPage;

                    using (this.unitOfWork.Begin())
                    {
                        this.unitOfWork.Current.CurrentSession.SetBatchSize(150);

                        foreach (var entity in entities)
                        {
                            this.ImportEntity(entity);
                        }
                    }
                }
                while (entities.Count > 0);
            }
            catch (GenericADOException exception)
            {
                Thread.Sleep(TimeSpan.FromMinutes(2).Milliseconds);
                Log.Application.ErrorFormat(exception.ToString());
                this.InternalExecute(currentStartRow, currentEndRow);
            }

            Console.WriteLine(string.Format(
                "{0} {1} importados em {2} segundos", this.count, typeof(TEntity).Name, timer.MiliSegundos / 1000));

            return this.itemsAlreadyImported;
        }
    }
}