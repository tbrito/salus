namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class AcessoDocumentoMap : ClassMap<AcessoDocumento>
    {
        public AcessoDocumentoMap()
        {
            this.Table("acessodocumento");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.AtorId, "autor_id");
            this.Map(x => x.Papel, "papel_id");
            this.References(x => x.TipoDocumento, "tipodocumento_id");
            
            this.DynamicUpdate();
        }
    }
}