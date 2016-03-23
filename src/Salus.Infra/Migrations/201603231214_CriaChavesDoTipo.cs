namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603231214)]
    public class CriaChavesDoTipo : Migration
    {
        public override void Up()
        {
            ////tipodocumento_id = 3 :: carta
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 3, nome = "cpf/cnpj", obrigatorio = true, tipodado = 5 });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 3, nome = "mes-ano", obrigatorio = true, tipodado = 1, mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))" });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 3, nome = "destinatario", obrigatorio = true, tipodado = 0 });

            ////tipodocumento_id = 4 :: declaracao
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 4, nome = "cpf/cnpj", obrigatorio = true, tipodado = 5 });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 4, nome = "mes-ano", obrigatorio = true, tipodado = 1, mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))" });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 4, nome = "solicitante", obrigatorio = true, tipodado = 0 });

            ////tipodocumento_id = 5 :: holerite
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 5, nome = "cpf/cnpj", obrigatorio = true, tipodado = 5 });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 5, nome = "mes-ano", obrigatorio = true, tipodado = 1, mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))" });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 5, nome = "colaborador", obrigatorio = true, tipodado = 0 });

            ////tipodocumento_id = 6 :: folha ponto
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 6, nome = "cpf/cnpj", obrigatorio = true, tipodado = 5 });
            this.Insert.IntoTable("chaves")
                .Row(new { tipodocumento_id = 6, nome = "mes-ano", obrigatorio = true, tipodado = 1, mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))" });
        }

        public override void Down()
        {
        }
    }
}
