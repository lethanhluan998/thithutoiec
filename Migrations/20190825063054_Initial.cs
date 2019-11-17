using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiThuToiec2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 60, nullable: false),
                    Password = table.Column<string>(maxLength: 60, nullable: false),
                    Mail = table.Column<string>(nullable: false),
                    Sdt = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoanVanAudioImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoanVan = table.Column<string>(nullable: true),
                    Audio = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    PartID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanVanAudioImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TienDoNguoiDungs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountID = table.Column<int>(nullable: false),
                    thangDiem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienDoNguoiDungs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TienDoNguoiDungs_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoanVanAudioImageID = table.Column<int>(nullable: false),
                    cauHoi = table.Column<string>(nullable: true),
                    DAA = table.Column<string>(nullable: true),
                    DAB = table.Column<string>(nullable: true),
                    DAC = table.Column<string>(nullable: true),
                    DAD = table.Column<string>(nullable: true),
                    DADung = table.Column<string>(nullable: true),
                    NumBer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHois_DoanVanAudioImages_DoanVanAudioImageID",
                        column: x => x.DoanVanAudioImageID,
                        principalTable: "DoanVanAudioImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartName = table.Column<string>(nullable: true),
                    TestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parts_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuNguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TienDoNguoiDungID = table.Column<int>(nullable: false),
                    TestID = table.Column<int>(nullable: false),
                    Diem = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuNguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuNguoiDungs_TienDoNguoiDungs_TienDoNguoiDungID",
                        column: x => x.TienDoNguoiDungID,
                        principalTable: "TienDoNguoiDungs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauTraLois",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LichSuNguoiDungID = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(nullable: true),
                    IdCauHoi = table.Column<int>(nullable: false),
                    DAChon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauTraLois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauTraLois_LichSuNguoiDungs_LichSuNguoiDungID",
                        column: x => x.LichSuNguoiDungID,
                        principalTable: "LichSuNguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_DoanVanAudioImageID",
                table: "CauHois",
                column: "DoanVanAudioImageID");

            migrationBuilder.CreateIndex(
                name: "IX_CauTraLois_LichSuNguoiDungID",
                table: "CauTraLois",
                column: "LichSuNguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuNguoiDungs_TienDoNguoiDungID",
                table: "LichSuNguoiDungs",
                column: "TienDoNguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_TestID",
                table: "Parts",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_TienDoNguoiDungs_AccountID",
                table: "TienDoNguoiDungs",
                column: "AccountID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "CauTraLois");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "DoanVanAudioImages");

            migrationBuilder.DropTable(
                name: "LichSuNguoiDungs");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "TienDoNguoiDungs");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
