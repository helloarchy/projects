using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.API.Migrations
{
    public partial class ExtendProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<string>(
                name: "FullDescriptionMdx",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescriptionMdx",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Projects",
                newName: "Description");
        }
    }
}
