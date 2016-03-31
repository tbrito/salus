namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class AreaMap : ClassMap<Area>
    {
        public AreaMap()
        {
            this.Table("area");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome");
            this.Map(x => x.Ativa, "ativo");
            this.Map(x => x.Abreviacao, "abreviacao");
            this.Map(x => x.Segura, "segura");
            this.References(x => x.Parent).Column("parent_id");

            this.DynamicUpdate();
        }
    }
}