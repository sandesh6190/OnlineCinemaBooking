using System.Data.Common;

namespace SimpleAuth.ViewModels.Tickets;
public class TicketBookedSuccessVm
{
    public long? TicketId { get; set; }

    public string MovieName { get; set; }
    public string BookedBy { get; set; }
    public DateTime BookedTime { get; set; }
    public DateTime ShowTime { get; set; }
    public decimal PerTicketCost { get; set; }
    // public decimal TotalCost { get; set; }
    public List<ShowSeatName> ShowSeatNames { get; set; }
    //QRCode
}

public class ShowSeatName
{
    public string SeatName { get; set; }
}