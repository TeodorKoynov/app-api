using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class ActivePlayingSonginDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSong_Songs_SongId",
                table: "ActivePlayingSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSong_AspNetUsers_UserId",
                table: "ActivePlayingSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivePlayingSong",
                table: "ActivePlayingSong");

            migrationBuilder.RenameTable(
                name: "ActivePlayingSong",
                newName: "ActivePlayingSongs");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSong_UserId",
                table: "ActivePlayingSongs",
                newName: "IX_ActivePlayingSongs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSong_SongId",
                table: "ActivePlayingSongs",
                newName: "IX_ActivePlayingSongs_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSong_PlaylistId",
                table: "ActivePlayingSongs",
                newName: "IX_ActivePlayingSongs_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivePlayingSongs",
                table: "ActivePlayingSongs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSongs_Songs_SongId",
                table: "ActivePlayingSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSongs_AspNetUsers_UserId",
                table: "ActivePlayingSongs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSongs_Playlists_PlaylistId",
                table: "ActivePlayingSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSongs_Songs_SongId",
                table: "ActivePlayingSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivePlayingSongs_AspNetUsers_UserId",
                table: "ActivePlayingSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivePlayingSongs",
                table: "ActivePlayingSongs");

            migrationBuilder.RenameTable(
                name: "ActivePlayingSongs",
                newName: "ActivePlayingSong");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSongs_UserId",
                table: "ActivePlayingSong",
                newName: "IX_ActivePlayingSong_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSongs_SongId",
                table: "ActivePlayingSong",
                newName: "IX_ActivePlayingSong_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivePlayingSongs_PlaylistId",
                table: "ActivePlayingSong",
                newName: "IX_ActivePlayingSong_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivePlayingSong",
                table: "ActivePlayingSong",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSong_Playlists_PlaylistId",
                table: "ActivePlayingSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSong_Songs_SongId",
                table: "ActivePlayingSong",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivePlayingSong_AspNetUsers_UserId",
                table: "ActivePlayingSong",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
