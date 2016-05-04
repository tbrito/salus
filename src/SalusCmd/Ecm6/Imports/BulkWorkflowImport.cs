namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Infra.Extensions;

    public class BulkWorkflowImport : BulkImport<InflowDto, Workflow>
    {        
        protected override string TableName
        {
            get
            {
                return "workflow";
            }
        }

        protected override Workflow ConvertDtoToEntity(InflowDto dto)
        {
            Usuario de;
            Usuario para;

            if (string.IsNullOrEmpty(dto.FromUsr) == false)
            {
                de = this.Create<Usuario>(dto.FromUsr);
            }
            else
            {
                de = this.Create<Usuario>("1");
            }

            if (string.IsNullOrEmpty(dto.Usr) == false)
            {
                para = this.Create<Usuario>(dto.Usr);
            }
            else
            {
                para = this.Create<Usuario>("1");
            }

            return new Workflow
            {
                Id = dto.Id,
                Documento = new Documento { Id = dto.DocCode.ToInt() },
                CriadoEm = dto.Date1,
                De = de,
                Para = para
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from workflow");
        }

        protected override IEnumerable<InflowDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, inflow_code) Id,
    doc_code DocCode,
    inflow_area Area,
    inflow_fromusr FromUsr,
    inflow_date1 Date1,
    inflow_usr Usr
from 
    inflow
order by
    inflow_code";

            return connEcm6.Query<InflowDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("workflow");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("documento_id", typeof(int)));
            dt.Columns.Add(new DataColumn("de", typeof(int)));
            dt.Columns.Add(new DataColumn("para", typeof(int)));
            dt.Columns.Add(new DataColumn("criadoem", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("lido", typeof(bool)));
            dt.Columns.Add(new DataColumn("status", typeof(int)));
            dt.Columns.Add(new DataColumn("mensagem"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["documento_id"] = this.GetId(entity.Documento);
                row["de"] = this.GetId(entity.De);
                row["para"] = this.GetId(entity.Para);
                row["criadoem"] = entity.CriadoEm;
                row["lido"] = true;
                row["status"] = 2;
                row["mensagem"] = "(vazio)";

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}