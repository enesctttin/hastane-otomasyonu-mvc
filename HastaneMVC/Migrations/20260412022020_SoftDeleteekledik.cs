using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneMVC.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteekledik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TahlilTurleri",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RandevuTahlilleri",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hastalar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Doktorlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Branslar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TahlilTurleri");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RandevuTahlilleri");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Branslar");
        }
    }
}
