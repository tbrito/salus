namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class VersaoDocumentoMap : ClassMap<VersaoDocumento>
    {
        public VersaoDocumentoMap()
        {
            this.Table("versaodocumento");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Comentario, "comentario").Nullable();
            this.Map(x => x.Versao, "versao");
            this.Map(x => x.CriadoEm, "criadoem");
            this.References(x => x.CriadoPor, "usuario_id").Nullable();
            this.References(x => x.Documento, "documento_id").Nullable();
            
            this.DynamicUpdate();
        }
    }
}