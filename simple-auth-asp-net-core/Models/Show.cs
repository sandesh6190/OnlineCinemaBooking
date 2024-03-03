using System.ComponentModel.DataAnnotations.Schema;
using SimpleAuth.Constants;
using SimpleAuth.Setup;

namespace SimpleAuth.Models;
public class Show
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public virtual Movie Movies { get; set; }
    public long RoomId { get; set; }
    public virtual Room Rooms { get; set; }
    public DateTime ShowTime { get; set; }
    public string ShowStatus { get; set; } = ShowStatusConstants.Available;
    public int Cost { get; set; }
    public DateTime DateModified { get; set; }

}
