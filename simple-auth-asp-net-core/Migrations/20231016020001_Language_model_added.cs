using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class Language_model_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Languages");
        }
    }
}
