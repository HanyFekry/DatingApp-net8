using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addLanguageArEnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnName",
                table: "Users");
        }
    }
}
