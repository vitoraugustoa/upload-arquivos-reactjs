using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadArquivosAPI.Migrations
{
    public partial class ArquivoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Url = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");
        }
    }
}
