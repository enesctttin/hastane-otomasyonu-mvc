using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneMVC.Migrations
{
    /// <inheritdoc />
    public partial class Create_Delete_Time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "TahlilTurleri",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "TahlilTurleri",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "RandevuTahlilleri",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "RandevuTahlilleri",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Randevular",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Randevular",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Hastalar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Hastalar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Doktorlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Doktorlar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Branslar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Branslar",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "TahlilTurleri");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "TahlilTurleri");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "RandevuTahlilleri");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "RandevuTahlilleri");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Branslar");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Branslar");
        }
    }
}
