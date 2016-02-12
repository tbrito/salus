namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class PerfilMap : ClassMap<Perfil>
    {
        public PerfilMap()
        {
            this.Table("pefil");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome");
            
            this.DynamicUpdate();
        }
    }
}