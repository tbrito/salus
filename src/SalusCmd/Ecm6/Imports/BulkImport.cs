namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Salus.Model.Entidades;
    using SalusCmd.Ecm6;
    using Salus.Infra.ConnectionInfra;
    using Salus.Infra.Logs;

    public abstract class BulkImport<TDto, TEntity> where TDto : Entidade
    {
        protected List<TEntity> entities;
        protected HashSet<int> existIds;

        protected abstract string TableName
        {
            get;
        }

        public void Execute(string message)
        {
            using (new Measure(message))
            {
                DapperHelper.UsingEcm6Connection(
                    connEcm6 => DapperHelper.UsingConnection(
                        conn => this.InternalExecute(connEcm6, conn)));
            } 
        }

        protected abstract IEnumerable<int> GetExists(IDbConnection conn);

        protected abstract IEnumerable<TDto> GetDtos(IDbConnection connEcm6);

        protected abstract DataTable CreateDataTable();

        protected abstract TEntity ConvertDtoToEntity(TDto dto);

        protected object GetId(Entidade entidade)
        {
            if (entidade == null)
            {
                return DBNull.Value;
            }

            return entidade.Id;
        }

        protected object GetDate(DateTime? value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        protected virtual void AfterExecute()
        {
        }

        protected void BulkCopy(DataTable dt, string tableName)
        {
            var copy = new SqlBulkCopy(
                BancoDeDados.ObterConnectionString(),
                SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.TableLock)
            {
                BatchSize = 2000,
                BulkCopyTimeout = 21600,
                DestinationTableName = tableName
            };

            foreach (DataColumn column in dt.Columns)
            {
                copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }

            copy.NotifyAfter = dt.Rows.Count / 50;
            copy.SqlRowsCopied += (sender, args) =>
            {
                Console.CursorLeft = 0;
                Console.Write(((100 * args.RowsCopied) / dt.Rows.Count) + " %     ");
            };

            copy.WriteToServer(dt);

            Console.CursorLeft = 0;
            Console.Write("100 %     ");
            Console.WriteLine();
        }

        private void InternalExecute(IDbConnection connEcm6, IDbConnection conn)
        {
            this.existIds = new HashSet<int>();

            Log.App.DebugFormat("Pesquisando itens já importados");

            foreach (var contentId in this.GetExists(conn))
            {
                this.existIds.Add(contentId);
            }

            Log.App.DebugFormat("Encontrado {0} itens já importados", this.existIds.Count);

            this.entities = new List<TEntity>();
            var dtosCount = 0;

            foreach (var dto in this.GetDtos(connEcm6))
            {
                dtosCount++;

                if (dtosCount % 10000 == 0)
                {
                    Console.CursorLeft = 0;
                    Console.Write(dtosCount);
                }

                if (this.existIds.Contains(dto.Id))
                {
                    continue;
                }

                var entity = this.ConvertDtoToEntity(dto);
                this.entities.Add(entity);
            }

            Console.CursorLeft = 0;
            Console.WriteLine(dtosCount);

            if (this.entities.Count == 0)
            {
                Log.App.Info("Não há itens a serem migrados");
                return;
            }

            //// faz o bulk copy
            var dt = this.CreateDataTable();
            this.BulkCopy(dt, this.TableName);

            this.AfterExecute();
        }
    }
}