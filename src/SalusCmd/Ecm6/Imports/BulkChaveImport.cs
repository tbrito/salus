namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Model;

    public class BulkChaveImport : BulkImport<KeyDefDto, Chave>
    {
        protected override string TableName
        {
            get
            {
                return "chaves";
            }
        }

        protected override Chave ConvertDtoToEntity(KeyDefDto dto)
        {
            return new Chave
            {
                Id = dto.Id,
                TipoDocumento = this.Create<TipoDocumento>(dto.GroupDocCode),
                Obrigatorio = dto.Obrig == "S",
                Ativo = true,
                Nome = dto.Descricao,
                TipoDado = TipoDado.FromInt32(dto.TipoDadoId)
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from chaves");
        }

        protected override IEnumerable<KeyDefDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, keydef_code) Id,
    groupdoc_code GroupDocCode, 
    keydef_obrig Obrig,
    keydef_desc Descricao,
    CONVERT(INT, keytype_code) TipoDadoId
from 
    keydef
order by
    keydef_code";

            return connEcm6.Query<KeyDefDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("chaves");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("tipodocumento_id", typeof(int)));
            dt.Columns.Add(new DataColumn("nome", typeof(string)));
            dt.Columns.Add(new DataColumn("obrigatorio", typeof(bool)));
            dt.Columns.Add(new DataColumn("ativo", typeof(bool)));
            dt.Columns.Add(new DataColumn("tipodado", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["tipodocumento_id"] = this.GetId(entity.TipoDocumento);
                row["tipodado"] = entity.TipoDado;
                row["obrigatorio"] = entity.Obrigatorio;
                row["nome"] = entity.Nome;
                row["ativo"] = entity.Ativo;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}