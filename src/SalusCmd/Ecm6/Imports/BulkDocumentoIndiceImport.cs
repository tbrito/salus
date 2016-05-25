namespace SalusCmd.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using System.Linq;

    public class BulkDocumentoIndiceImport : BulkImport<FileDto, Documento>
    {
        protected override string TableName
        {
            get
            {
                return "documento";
            }
        }

        protected override Documento ConvertDtoToEntity(FileDto dto)
        {
            var documento = new Documento();

            documento.Id = dto.Id;
            documento.DataCriacao = dto.Date;
            documento.TipoDocumento = this.Create<TipoDocumento>(dto.GroupDocCode);
            
            if (string.IsNullOrEmpty(dto.Usr) == false)
            {
                documento.Usuario = this.Create<Usuario>(dto.Usr);
            }
            else
            {
                documento.Usuario = this.Create<Usuario>("5");
            }
            
            documento.SearchStatus = SearchStatus.DontIndex;
            documento.Assunto = this.GetTipoDocumentoDescricao(dto.GroupDocCode);
            
            return documento;
        }

        private string GetTipoDocumentoDescricao(string groupDocCode)
        {
            string tipoDocumentoNome = "(Sem Assunto)";

            if (string.IsNullOrEmpty(groupDocCode))
            {
                return tipoDocumentoNome;
            }

            DapperHelper.UsingConnection(conn => {
                var tipoDocumento = conn.Query<string>("select nome from tipodocumento where id = " + groupDocCode);
                if (tipoDocumento.Count() > 0)
                {
                    tipoDocumentoNome = tipoDocumento.ElementAt(0);
                }
            });

            return tipoDocumentoNome;
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from documento", conn);
        }

        protected override IEnumerable<FileDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, doc_code) Id,
	groupdoc_code GroupDocCode, 
	doc_usr Usr, 
	doc_date Date, 
	doc_index_usr IndexUsr, 
	doc_index_date IndexDate,
	doc_filetype Extension
from 
	doc
where
	doc_filetype = '-IDX-'
order by
    doc_code";

            return connEcm6.Query<FileDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("documento");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("assunto"));
            dt.Columns.Add(new DataColumn("tipodocumento_id", typeof(int)));
            dt.Columns.Add(new DataColumn("user_id", typeof(int)));
            dt.Columns.Add(new DataColumn("criadoem", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("tamanho"));
            dt.Columns.Add(new DataColumn("search_status", typeof(int)));
            dt.Columns.Add(new DataColumn("eh_indice", typeof(bool)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["assunto"] = entity.Assunto;
                row["tipodocumento_id"] = this.GetId(entity.TipoDocumento);
                row["user_id"] = this.GetId(entity.Usuario);
                row["criadoem"] = this.GetDate(entity.DataCriacao);
                row["tamanho"] = entity.Tamanho;
                row["search_status"] = 0;
                row["eh_indice"] = true;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private bool GrupoDocumentoValido(FileDto fileDto)
        {
            return string.IsNullOrEmpty(fileDto.GroupDocCode) == false &&
                fileDto.GroupDocCode.ToLower() != "null";
        }
    }
}