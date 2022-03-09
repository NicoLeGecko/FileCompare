using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileCompare.Migrations
{
    public partial class RenamedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHash",
                table: "FileEntries");

            migrationBuilder.RenameColumn(
                name: "HashBytes",
                table: "FileEntries",
                newName: "HashAsBytes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashAsBytes",
                table: "FileEntries",
                newName: "HashBytes");

            migrationBuilder.AddColumn<string>(
                name: "ContentHash",
                table: "FileEntries",
                type: "TEXT",
                nullable: true);
        }
    }
}
