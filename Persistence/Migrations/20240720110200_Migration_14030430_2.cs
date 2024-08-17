using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Migration_14030430_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agent_Id",
                table: "Nazm_tspagents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Branch_Id",
                table: "Nazm_tspagents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Invoice_Model",
                table: "Nazm_tspagents",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mapfylddtlcod",
                table: "Nazm_tspagents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent_Id",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "Branch_Id",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "Invoice_Model",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "Mapfylddtlcod",
                table: "Nazm_tspagents");
        }
    }
}
