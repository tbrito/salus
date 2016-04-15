namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class AcessoFuncionalidadeMap : ClassMap<AcessoFuncionalidade>
    {
        public AcessoFuncionalidadeMap()
        {
            this.Table("acessofuncionalidade");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.AtorId, "autor_id").CustomType<int>();
            this.Map(x => x.Papel, "papel_id");
            this.Map(x => x.Funcionalidade, "funcionalidade_id");
            
            this.DynamicUpdate();
        }
    }
}