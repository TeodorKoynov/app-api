using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class ActivePlayingSongEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
