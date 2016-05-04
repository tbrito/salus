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
            this.Map(x => x.Login, "nome");
            this.Map(x => x.Nome, "nome_completo");
            this.Map(x => x.Email, "email");
            this.Map(x => x.Senha, "senha");
            this.Map(x => x.Expira, "expira");
            this.Map(x => x.Ativo, "ativo");
            this.Map(x => x.MotivoInatividade, "motivo_inatividade");
            this.Map(x => x.ExpiraEm, "expira_em");
            this.Map(x => x.Avatar, "avatar");
            this.References(x => x.Perfil).Column("perfil_id");
            this.References(x => x.Area).Column("area_id");

            this.DynamicUpdate();
        }
    }
}