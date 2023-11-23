using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class Adding_DateModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Genres",
                newName: "DateModified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Genres",
                newName: "CreatedDate");
        }
    }
}
