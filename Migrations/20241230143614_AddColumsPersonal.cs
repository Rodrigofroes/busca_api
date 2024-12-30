using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackAppPersonal.Migrations
{
    /// <inheritdoc />
    public partial class AddColumsPersonal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Personais",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Personais",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Personais");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Personais");
        }
    }
}
