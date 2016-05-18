namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using NHibernate;
    using Salus.Model.Entidades;
    using Salus.Infra.Repositorios;
    using Salus.Infra.Logs;
    using Salus.Infra.Extensions;
    using Salus.Model.Entidades.Import;

    public class DocumentoPreindexadoImport : PaginatedImportBase<Documento>
    {
        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_predoc";
            }
        }

        public override List<Documento> GetEntities(IStatelessSession session, int startRow, int endRow)
        {
            const string Sql = @"
select
    ROW_NUMBER() over (order by predoc_code) as RowNum, 
    predoc_code Code, 
    groupdoc_code GroupDocCode, 
    predoc_usr Usr, 
    predoc_date Date
from 
    predoc
where 
	predoc_processed = 'N'";

            var documentos = new List<Documento>();

            var dtos = session
                .CreateSQLQuery(this.GetPaginatedSql(Sql, startRow, endRow))
                .SetResultTransformer(CustomResultTransformer<PreIndexedFileDto>.Do())
                .List<PreIndexedFileDto>();

            foreach (var preIndexedFileDto in dtos)
            {
                Log.App.DebugFormat("Importando PreIndexedFileDto #{0}", preIndexedFileDto.Code);

                var documento = new Documento();

                documento.Id = preIndexedFileDto.Code.ToInt();
                documento.DataCriacao = preIndexedFileDto.Date;

                if (string.IsNullOrEmpty(preIndexedFileDto.Usr) == false)
                {
                    documento.Usuario = this.Create<Usuario>(preIndexedFileDto.Usr);
                }
                else
                {
                    documento.Usuario = this.Create<Usuario>("1");
                }

                documento.TipoDocumento = this.Create<TipoDocumento>(preIndexedFileDto.GroupDocCode);
                documento.SearchStatus = SearchStatus.DontIndex;
                documento.EhPreIndexacao = true;
                documento.Assunto = "(sem assunto)";

                documentos.Add(documento);
            }

            return documentos;
        }

        public override void AfterImport(int oldId, Documento entity, ISession session)
        {
            session.Save(new Ecm6PreDoc
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }

        public override bool ShouldImport(Documento entity)
        {
            return this.CheckIfExist<Ecm6PreDoc>(entity);
        }
    }
}