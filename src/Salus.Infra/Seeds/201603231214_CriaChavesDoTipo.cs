namespace Salus.Infra.Seeds
{
    using FluentMigrator;
    using Model;
    using Model.Entidades;

    [Migration(201603231214)]
    public class CriaChavesDoTipo : Migration
    {
        private TipoDocumento administrativo;
        private TipoDocumento dp;

        public override void Up()
        {
            administrativo = new TipoDocumento
            {
                Nome = "Administrativo",
                Ativo = true,
                EhPasta = true,
            }.Persistir();

            dp = new TipoDocumento
            {
                Nome = "Departamento Pessoal",
                Ativo = true,
                EhPasta = true
            }.Persistir();

            this.CriaTipoCarta();
            this.CriaTipoDeclaracao();
            this.CriaTipoHolerite();
            this.CriaFolhaPonto();
        }

        private void CriaFolhaPonto()
        {
            var folhaPonto = new TipoDocumento
            {
                Nome = "folha de ponto",
                Ativo = true,
                EhPasta = false,
                Parent = dp
            }.Persistir();

            new Chave
            {
                TipoDocumento = folhaPonto,
                Nome = "cpf/cnpj",
                Obrigatorio = true,
                TipoDado = TipoDado.CpfCnpj
            }.Persistir();

            new Chave
            {
                TipoDocumento = folhaPonto,
                Nome = "mes-ano",
                Obrigatorio = true,
                TipoDado = TipoDado.Mascara,
                Mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))"
            }.Persistir();
        }

        private void CriaTipoHolerite()
        {
            var holerite = new TipoDocumento
            {
                Nome = "holerite",
                Ativo = true,
                EhPasta = false,
                Parent = dp
            }.Persistir();

            new Chave
            {
                TipoDocumento = holerite,
                Nome = "cpf/cnpj",
                Obrigatorio = true,
                TipoDado = TipoDado.CpfCnpj
            }.Persistir();

            new Chave
            {
                TipoDocumento = holerite,
                Nome = "mes-ano",
                Obrigatorio = true,
                TipoDado = TipoDado.Mascara,
                Mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))"
            }.Persistir();

            new Chave
            {
                TipoDocumento = holerite,
                Nome = "colaborador",
                Obrigatorio = true,
                TipoDado = TipoDado.Texto
            }.Persistir();
        }

        private void CriaTipoDeclaracao()
        {
            var declaracao = new TipoDocumento
            {
                Nome = "declaração",
                Ativo = true,
                EhPasta = false,
                Parent = administrativo
            }.Persistir();

            new Chave
            {
                TipoDocumento = declaracao,
                Nome = "cpf/cnpj",
                Obrigatorio = true,
                TipoDado = TipoDado.CpfCnpj
            }.Persistir();

            new Chave
            {
                TipoDocumento = declaracao,
                Nome = "mes-ano",
                Obrigatorio = true,
                TipoDado = TipoDado.Mascara,
                Mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))"
            }.Persistir();

            new Chave
            {
                TipoDocumento = declaracao,
                Nome = "solicitante",
                Obrigatorio = true,
                TipoDado = TipoDado.Lista
            }.Persistir();
        }

        private void CriaTipoCarta()
        {
            var carta = new TipoDocumento
            {
                Nome = "carta",
                Ativo = true,
                EhPasta = false,
                Parent = administrativo
            }.Persistir();

            new Chave
            {
                TipoDocumento = carta,
                Nome = "cpf/cnpj",
                Obrigatorio = true,
                TipoDado = TipoDado.Texto
            }.Persistir();

            new Chave
            {
                TipoDocumento = carta,
                Nome = "mes-ano",
                Obrigatorio = true,
                TipoDado = TipoDado.Mascara,
                Mascara = @"(((0[123456789]|10|11|12)([/])(([1][9][0-9][0-9])|([2][0-9][0-9][0-9]))))"
            }.Persistir();

            new Chave
            {
                TipoDocumento = carta,
                Nome = "destinatario",
                Obrigatorio = true,
                TipoDado = TipoDado.Texto
            }.Persistir();
        }

        public override void Down()
        {
        }
    }
}
