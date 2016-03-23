namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class ChaveMap : ClassMap<Chave>
    {
        public ChaveMap()
        {
            this.Table("chaves");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Nome, "nome").Nullable();
            this.Map(x => x.ItensLista, "itens");
            this.Map(x => x.Obrigatorio, "obrigatorio");
            this.Map(x => x.Mascara, "mascara").Nullable();
            this.Map(x => x.TipoDado, "tipodado").CustomType<int>().Nullable();
            this.References(x => x.TipoDocumento, "tipodocumento_id");

            this.DynamicUpdate();
        }
    }
}