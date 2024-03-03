using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.TicketSeatsAdminPanel;
public class TicketSeatsAdminIndexVm
{
    public List<TicketSeat> TicketSeats;
    public string SeatName { get; set; }
}
