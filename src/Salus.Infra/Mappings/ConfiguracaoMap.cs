namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class ConfiguracaoMap : ClassMap<Configuracao>
    {
        public ConfiguracaoMap()
        {
            this.Table("configuracao");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Chave, "chave");
            this.Map(x => x.Valor, "valor");

            this.DynamicUpdate();
        }
    }
}