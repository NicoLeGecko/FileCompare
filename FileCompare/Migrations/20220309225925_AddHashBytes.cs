using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileCompare.Migrations
{
    public partial class AddHashBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "HashBytes",
                table: "FileEntries",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashBytes",
                table: "FileEntries");
        }
    }
}
