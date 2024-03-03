using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAuth.Migrations
{
    /// <inheritdoc />
    public partial class TicketShowSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TicketSeats_ShowSeatId",
                table: "TicketSeats",
                column: "ShowSeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSeats_ShowSeats_ShowSeatId",
                table: "TicketSeats",
                column: "ShowSeatId",
                principalTable: "ShowSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketSeats_ShowSeats_ShowSeatId",
                table: "TicketSeats");

            migrationBuilder.DropIndex(
                name: "IX_TicketSeats_ShowSeatId",
                table: "TicketSeats");
        }
    }
}
