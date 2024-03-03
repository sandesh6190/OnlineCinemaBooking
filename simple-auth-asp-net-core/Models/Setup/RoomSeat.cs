namespace SimpleAuth.Setup;
public class RoomSeat
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Position { get; set; }
    public bool IsLastSeat { get; set; }
    public DateTime DateModified { get; set; }
    public long RoomId { get; set; }
    public virtual Room Rooms { get; set; }
}
