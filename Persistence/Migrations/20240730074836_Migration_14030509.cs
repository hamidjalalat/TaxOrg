using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Migration_14030509 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice_Model",
                table: "Nazm_tspagents");

            migrationBuilder.AddColumn<int>(
                name: "DimProduct_Id",
                table: "Nazm_tspagents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceModel_Id",
                table: "Nazm_tspagents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Netsale_Id",
                table: "Nazm_tspagents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Policy_No",
                table: "Nazm_tspagents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DimProduct_Id",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "InvoiceModel_Id",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "Netsale_Id",
                table: "Nazm_tspagents");

            migrationBuilder.DropColumn(
                name: "Policy_No",
                table: "Nazm_tspagents");

            migrationBuilder.AddColumn<string>(
                name: "Invoice_Model",
                table: "Nazm_tspagents",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }
    }
}
