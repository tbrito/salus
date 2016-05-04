namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades.Import;

    public class Ecm6AccessDocMap : ClassMap<Ecm6AccessDoc>
    {
        public Ecm6AccessDocMap()
        {
            this.Table("ecm6_accessdoc");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Ecm6Id, "ecm6_id");
            this.Map(x => x.Ecm8Id, "ecm8_id");

            this.DynamicUpdate();
        }
    }
}