using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Migration_14030322 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nazm_tspagents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    indati2m = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tax17 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    acn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    am = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bpn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bsrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cfee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    consfee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    crn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    exr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    iinn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    indatim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inno = table.Column<int>(type: "int", nullable: false),
                    inp = table.Column<int>(type: "int", nullable: false),
                    ins = table.Column<int>(type: "int", nullable: false),
                    inty = table.Column<int>(type: "int", nullable: false),
                    irtaxid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    olam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    olr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    olt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pcn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sbc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scln = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setm = table.Column<int>(type: "int", nullable: false),
                    spro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sscv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ssrv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sstid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sstt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tinb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tob = table.Column<int>(type: "int", nullable: false),
                    trmn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ft = table.Column<int>(type: "int", nullable: false),
                    input_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Retry = table.Column<int>(type: "int", nullable: false),
                    InputForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Update_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error_Code = table.Column<int>(type: "int", nullable: false),
                    Create_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nazm_tspagents", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nazm_tspagents");
        }
    }
}
