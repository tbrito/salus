namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Entities.Import;
    using NHibernate;
    using Veros.Data;
    using Veros.Data.Hibernate;
    using Veros.Ecm.Model.Entities;
    using Veros.Framework;
    using Veros.Framework.Modelo;
    using Veros.Framework.Performance;

    public abstract class ImportBase<TEntity> 
        where TEntity : Entidade
    {
        protected readonly IUnitOfWork unitOfWork;
        protected Dictionary<int, TEntity> itemsForReference;
        protected TEntity entidadeImportada;
        protected int count;
        
        protected ImportBase()
        {
            this.unitOfWork = IoC.Current.Resolve<IUnitOfWork>();
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
            Log.Application.InfoFormat(message);

            IList<TEntity> entities = new List<TEntity>();

            Log.Application.DebugFormat("Obtendo registros no ecm 6");

            var timer = new Medicao();
            
            ImportDatabase.Using(session =>
            {
                entities = this.GetEntities(session).ToList();
            });

            Log.Application.DebugFormat("Encontrado {0} registros", entities.Count);
            Log.Application.DebugFormat("Importando registros no ecm 8...");

            foreach (var entity in entities)
            {
                using (this.unitOfWork.Begin())
                {
                    this.unitOfWork.Current.CurrentSession.SetBatchSize(500);
                    try
                    {   
                        this.ImportEntity(entity);
                    }
                    catch (System.Exception exception)
                    {
                        Log.Application.Error(string.Format("Erro ao importar {0} #{1}", entity.GetType().Name, entity.Id), exception);
                        throw;
                    } 
                }
            }
              
            Console.WriteLine(
                string.Format(
                "{0} {1} importados em {2} segundos", 
                this.count, 
                typeof(TEntity).Name, 
                timer.MiliSegundos / 1000));

            return this.itemsForReference;         
        }

        protected bool CheckIfExist<TDepara>(Entidade entity) where TDepara : Ecm6ToEcm8
        {
            var depara = this.unitOfWork.Current.CurrentSession
                .CreateSQLQuery(this.SqlEcm6)
                .SetInt32("ecm6Id", entity.Id)
                .SetResultTransformer(CustomResultTransformer<TDepara>.Do())
                .SetTimeout(0)
                .UniqueResult<TDepara>();

            if (depara == null)
            {
                return true;
            }

            this.entidadeImportada = this.unitOfWork.Current.CurrentSession.Get<TEntity>(depara.Ecm8Id);

            return false;
        }

        protected void ImportEntity(TEntity entity)
        {
            var oldId = entity.Id;

            if (this.ShouldImport(entity) == false)
            {
                Log.Application.DebugFormat("{0} Já Importado. Id #{1}", entity.GetType().Name, entity.Id);
                this.itemsForReference.Add(oldId, this.entidadeImportada);
                return;
            }

            entity.Id = 0;

            this.BeforeImport(oldId, entity);
            this.unitOfWork.Current.CurrentSession.Save(entity);
            this.AfterImport(oldId, entity, this.unitOfWork.Current.CurrentSession);
            this.itemsForReference.Add(oldId, entity);
            this.count++;
        }
    }
}