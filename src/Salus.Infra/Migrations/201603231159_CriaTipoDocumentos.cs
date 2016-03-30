namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603231159)]
    public class CriaTipoDocumentos : Migration
    {
        public override void Up()
        {
            Alter.Table("tipodocumento").AddColumn("parent_id").AsInt32().Nullable();
            
            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "Administrativo", ativo = true, ehpasta = true });

            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "Departamento Pessoal", ativo = true, ehpasta = true });

            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "carta", ativo = true, ehpasta = false, parent_id = 1 });
            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "declaração", ativo = true, ehpasta = false, parent_id = 1 });

            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "holerite", ativo = true, ehpasta = false, parent_id = 2 });
            this.Insert.IntoTable("tipodocumento")
                .Row(new { nome = "folha de ponto", ativo = true, ehpasta = false, parent_id = 2 });
        }

        public override void Down()
        {
        }
    }
}
