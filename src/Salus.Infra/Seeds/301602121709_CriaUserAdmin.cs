namespace Salus.Infra.Seeds
{
    using FluentMigrator;
    using Model.Entidades;
    using Model.Servicos;

    [Migration(301602121709)]
    public class CriaUserAdmin : Migration
    {
        public override void Up()
        {
            var ccb = new Area
            {
                Nome = "CCB",
                Ativo = true,
                Abreviacao = "CCB",
                Parent = null,
                Segura = false,
            }.Persistir();

            var administrador = new Perfil
            {
                Nome = "Administrador",
                Ativo = true
            }.Persistir();

            new Usuario
            {
                Area = ccb,
                Perfil = administrador,
                Senha = new HashString().Do("pwd123"),
                Login = "admin",
                Nome = "Administrador do sistema",
                Email = "admin@ccb.com",
                Ativo = true,
                Expira = false
            }.Persistir();
        }

        public override void Down()
        {
        }
    }
}
