namespace Salus.Infra.Mappings
{
    using System;
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class TrilhaMap : ClassMap<Trilha>
    {
        public TrilhaMap()
        {
            this.Table("trilha");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Data, "data");
            this.Map(x => x.Descricao, "descricao");
            this.Map(x => x.Tipo, "tipo").CustomType<int>();
            this.References(x => x.Usuario, "user_id");

            this.DynamicUpdate();
        }

        private object Map(Func<Trilha, object> p, string v)
        {
            throw new NotImplementedException();
        }
    }
}