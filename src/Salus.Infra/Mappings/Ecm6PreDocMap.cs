﻿namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades.Import;

    public class Ecm6PreDocMap : ClassMap<Ecm6PreDoc>
    {
        public Ecm6PreDocMap()
        {
            this.Table("ecm6_predoc");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Ecm6Id, "ecm6_id");
            this.Map(x => x.Ecm8Id, "ecm8_id");

            this.DynamicUpdate();
        }
    }
}