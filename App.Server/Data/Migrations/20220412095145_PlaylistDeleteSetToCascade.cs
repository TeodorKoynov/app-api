using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class PlaylistDeleteSetToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
