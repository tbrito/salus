using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salus.Infra.Migrations
{
    [Migration(201602051009)]
    public class CriarAspNetUserRoles : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsAnsiString().NotNullable()
                .WithColumn("RoleId").AsAnsiString().NotNullable();

            Create.ForeignKey("FK_AspNetUserRoles_User")
                .FromTable("AspNetUserRoles")
                .ForeignColumn("UserId")
                .ToTable("AspNetUsers")
                .PrimaryColumn("Id");

            Create.ForeignKey("FK_AspNetUserRoles_Role")
                .FromTable("AspNetUserRoles")
                .ForeignColumn("RoleId")
                .ToTable("AspNetUsers")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("AspNetUserRoles");
        }
    }
}
