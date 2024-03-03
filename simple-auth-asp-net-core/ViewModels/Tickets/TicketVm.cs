using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.Tickets;
public class TicketVm
{
    public long ShowId { get; set; }
    public List<ShowSeatVm> ShowSeatVms { get; set; }

}

public class ShowSeatVm
{
    public long ShowSeatId { get; set; }

}
