using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class ActivePlayingSongEdit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivePlayingSong_PlaylistId",
                table: "ActivePlayingSong");

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistId",
                table: "ActivePlayingSong",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ActivePlayingSong_PlaylistId",
                table: "ActivePlayingSong",
                column: "PlaylistId",
                unique: true,
                filter: "[PlaylistId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivePlayingSong_PlaylistId",
                table: "ActivePlayingSong");

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistId",
                table: "ActivePlayingSong",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivePlayingSong_PlaylistId",
                table: "ActivePlayingSong",
                column: "PlaylistId",
                unique: true);
        }
    }
}
