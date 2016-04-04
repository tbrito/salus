namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class PerfilMap : ClassMap<Perfil>
    {
        public PerfilMap()
        {
            this.Table("perfil");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome");
            this.Map(x => x.Ativo, "ativo");

            this.DynamicUpdate();
        }
    }
}