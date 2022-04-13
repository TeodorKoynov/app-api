using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Server.Data.Migrations
{
    public partial class AudioFileIsDelitable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AudioFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AudioFiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AudioFiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AudioFiles");
        }
    }
}
