using SimpleAuth.Constants;

namespace SimpleAuth.Models;
public class ShowSeat
{
    public long Id { get; set; }
    public long ShowId { get; set; }
    public virtual Show Shows { get; set; }
    public int Position { get; set; }
    public string SeatName { get; set; }
    public bool IsLastSeatInRow { get; set; }
    public string SeatStatus { get; set; }
    public string BookedByPerson { get; set; }

}
