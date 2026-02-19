using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DesafioFinal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Horimetro = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusOperacional = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DataAquisicao = table.Column<DateOnly>(type: "date", nullable: true),
                    LocalizacaoAtual = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipamentos_Codigo",
                table: "equipamentos",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipamentos");
        }
    }
}
