using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class Table_Name_Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_user",
                table: "auth_user");

            migrationBuilder.RenameTable(
                name: "auth_user",
                newName: "Normal_User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Normal_User",
                table: "Normal_User",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Normal_User",
                table: "Normal_User");

            migrationBuilder.RenameTable(
                name: "Normal_User",
                newName: "auth_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_user",
                table: "auth_user",
                column: "Id");
        }
    }
}
