using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLy_KyTucXa.Migrations
{
    /// <inheritdoc />
    public partial class ThemKhoaNgoaiLichSuDongTien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MSSV",
                table: "LichSuDongTiens",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDongTiens_MSSV",
                table: "LichSuDongTiens",
                column: "MSSV");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuDongTiens_SinhViens_MSSV",
                table: "LichSuDongTiens",
                column: "MSSV",
                principalTable: "SinhViens",
                principalColumn: "MSSV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuDongTiens_SinhViens_MSSV",
                table: "LichSuDongTiens");

            migrationBuilder.DropIndex(
                name: "IX_LichSuDongTiens_MSSV",
                table: "LichSuDongTiens");

            migrationBuilder.AlterColumn<string>(
                name: "MSSV",
                table: "LichSuDongTiens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);
        }
    }
}
