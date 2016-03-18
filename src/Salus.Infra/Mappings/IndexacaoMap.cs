namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class IndexacaoMap : ClassMap<Indexacao>
    {
        public IndexacaoMap()
        {
            this.Table("indexacao");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Valor, "valor").Nullable();
            this.References(x => x.Documento, "documento_id");
            this.References(x => x.Chave, "chave_id");

            this.DynamicUpdate();
        }
    }
}