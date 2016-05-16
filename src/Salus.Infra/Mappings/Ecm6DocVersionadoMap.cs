namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades.Import;

    public class Ecm6DocVersionadoMap : ClassMap<Ecm6DocVersionado>
    {
        public Ecm6DocVersionadoMap()
        {
            this.Table("ecm6_docversionado");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Ecm6Id, "ecm6_id");
            this.Map(x => x.Ecm8Id, "ecm8_id");
            this.Map(x => x.ImportStatus, "import_status");

            this.DynamicUpdate();
        }
    }
}