using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class AlterSongDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "AudioFiles");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Songs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Songs");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "AudioFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
