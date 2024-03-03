namespace SimpleAuth.Models;
public class TicketSeat
{
    public long Id { get; set; }
    public string SeatName { get; set; }
    public long TicketId { get; set; }
    public virtual Ticket Tickets { get; set; }
    public long ShowSeatId { get; set; }
    public virtual ShowSeat ShowSeats { get; set; }

}
