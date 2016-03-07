﻿namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class DocumentoMap : ClassMap<Documento>
    {
        public DocumentoMap()
        {
            this.Table("documento");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.Assunto, "assunto");
            this.Map(x => x.DataCriacao, "criadoem");
            this.Map(x => x.Tamanho, "tamanho");
            
            this.DynamicUpdate();
        }
    }
}