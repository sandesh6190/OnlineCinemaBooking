using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class movieModelAltered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Trailer",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Trailer",
                table: "Movies");
        }
    }
}
