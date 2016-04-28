namespace Salus.Infra.Seeds
{
    using FluentMigrator;
    using Model;
    using Model.Entidades;

    [Migration(301604251835)]
    public class CriaConfiguracoes : Migration
    {
        public override void Up()
        {
            new Configuracao
            {
                Chave = "pesquisa.resultado.maximo.pagina",
                Valor = "15"
            }.Persistir();

            new Configuracao
            {
                Chave = "pesquisa.resultado.maximo",
                Valor = "200"
            }.Persistir();

            new Configuracao
            {
                Chave = "pesquisa.diretorio.indice",
                Valor = @"C:\Salus\IndicePesquisa"
            }.Persistir();
        }
        
        public override void Down()
        {
        }
    }
}
