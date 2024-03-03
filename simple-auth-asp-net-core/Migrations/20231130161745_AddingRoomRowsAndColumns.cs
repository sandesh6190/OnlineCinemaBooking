using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class AddingRoomRowsAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSeats",
                table: "Rooms",
                newName: "TotalRows");

            migrationBuilder.AddColumn<int>(
                name: "TotalColumns",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalColumns",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "TotalRows",
                table: "Rooms",
                newName: "TotalSeats");
        }
    }
}
