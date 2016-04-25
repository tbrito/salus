namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class DocumentoMap : ClassMap<Documento>
    {
        public DocumentoMap()
        {
            this.Table("documento");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Assunto, "assunto").Nullable();
            this.Map(x => x.DataCriacao, "criadoem");
            this.Map(x => x.Tamanho, "tamanho");
            this.Map(x => x.CpfCnpj, "cpfcnpj");
            this.Map(x => x.SearchStatus, "search_status").Nullable();
            this.References(x => x.Usuario, "user_id").Nullable();
            this.References(x => x.TipoDocumento, "tipodocumento_id").Nullable();

            this.HasMany(x => x.Indexacao)
                .KeyColumn("documento_id")
                .Inverse()
                .Cascade
                .None();

            this.DynamicUpdate();
        }
    }
}