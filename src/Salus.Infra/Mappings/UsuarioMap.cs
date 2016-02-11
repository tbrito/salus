namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            this.Table("usuario");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome");
            this.Map(x => x.Email, "email");
            this.Map(x => x.Senha, "senha");

            this.DynamicUpdate();
        }
    }
}