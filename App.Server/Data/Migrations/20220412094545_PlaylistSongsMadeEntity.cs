using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class PlaylistSongsMadeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlaylistSongs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PlaylistSongs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PlaylistSongs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlaylistSongs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
