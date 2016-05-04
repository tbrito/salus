namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Infra.Extensions;

    public class BulkVersaoDocumentoImport : BulkImport<ContentVersionDto, VersaoDocumento>
    {
        protected override string TableName
        {
            get
            {
                return "versaodocumento";
            }
        }

        protected override VersaoDocumento ConvertDtoToEntity(ContentVersionDto dto)
        {
            return new VersaoDocumento
            {
                Id = dto.Id,
                CriadoEm = dto.Data,
                CriadoPor = this.Create<Usuario>(dto.UsrCode),
                Comentario = dto.Obs,
                Documento = this.Create<Documento>(dto.DocCode),
                Versao = dto.Revisao.Split(new[] { '.' }, 1)[0].ToInt()
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from versaodocumento");
        }

        protected override IEnumerable<ContentVersionDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, vsdoc_code) Id,
    doc_code DocCode, 
    usr_code UsrCode, 
    vsdoc_data Data, 
    vsdoc_checkinobs Obs, 
    vsdoc_revisao Revisao
from 
    vsdoc
order by
    vsdoc_code";

            return connEcm6.Query<ContentVersionDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("versaodocumento");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("criadoem", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("usuario_id", typeof(int)));
            dt.Columns.Add(new DataColumn("documento_id", typeof(int)));
            dt.Columns.Add(new DataColumn("comentario"));
            dt.Columns.Add(new DataColumn("versao", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["criadoem"] = entity.CriadoEm;
                row["usuario_id"] = this.GetId(entity.CriadoPor);
                row["documento_id"] = this.GetId(entity.Documento);
                row["comentario"] = entity.Comentario;
                row["versao"] = entity.Versao;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}