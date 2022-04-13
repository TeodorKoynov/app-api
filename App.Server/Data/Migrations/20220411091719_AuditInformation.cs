using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class AuditInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PlaylistSongs",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Playlists",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AudioFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AudioFiles",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AudioFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AudioFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "AspNetUsers");
        }
    }
}
