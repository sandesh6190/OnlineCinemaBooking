using SimpleAuth.Constants;

namespace SimpleAuth.Models;
public class Ticket
{
    public long Id { get; set; }
    public DateTime BookedTime { get; set; }
    public string BookedBy { get; set; }
    public long ShowId { get; set; }
    public virtual Show Shows { get; set; }
    public int TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = PaymentStatusConstants.Pending;

}
