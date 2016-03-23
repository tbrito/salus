namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class TipoDocumentoMap : ClassMap<TipoDocumento>
    {
        public TipoDocumentoMap()
        {
            this.Table("tipodocumento");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome").Nullable();
            this.Map(x => x.EhPasta, "ehpasta");
            this.Map(x => x.Ativo, "ativo");
            this.References(x => x.Parent, "parent_id");

            this.DynamicUpdate();
        }
    }
}