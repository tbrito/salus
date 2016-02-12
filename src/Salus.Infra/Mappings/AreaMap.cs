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
            
            this.DynamicUpdate();
        }
    }
}