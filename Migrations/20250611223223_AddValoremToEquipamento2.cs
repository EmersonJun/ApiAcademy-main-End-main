using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AddValoremToEquipamento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Equipamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Equipamentos");
        }
    }
}
