using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiThuToiec2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PartID",
                table: "DoanVanAudioImages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartID",
                table: "DoanVanAudioImages",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
