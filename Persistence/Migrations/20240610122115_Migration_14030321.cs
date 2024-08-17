using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Migration_14030321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tspagents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Indatim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Indati2m = table.Column<int>(type: "int", nullable: false),
                    Tax17 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Acn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Am = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bbc = table.Column<int>(type: "int", nullable: false),
                    Bid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bpn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bsrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cfee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consfee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crn = table.Column<int>(type: "int", nullable: false),
                    Cut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dis = table.Column<int>(type: "int", nullable: false),
                    Exr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iinn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inp = table.Column<int>(type: "int", nullable: false),
                    Ins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inty = table.Column<int>(type: "int", nullable: false),
                    Irtaxid = table.Column<int>(type: "int", nullable: false),
                    Mu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Olam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Olr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Olt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pcn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sbc = table.Column<int>(type: "int", nullable: false),
                    Scc = table.Column<int>(type: "int", nullable: false),
                    Scln = table.Column<int>(type: "int", nullable: false),
                    Setm = table.Column<int>(type: "int", nullable: false),
                    Spro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sscv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssrv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sstid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sstt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tob = table.Column<int>(type: "int", nullable: false),
                    Trmn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vra = table.Column<int>(type: "int", nullable: false),
                    Tins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Retry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inputform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Update_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tspagents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tspagents");
        }
    }
}
