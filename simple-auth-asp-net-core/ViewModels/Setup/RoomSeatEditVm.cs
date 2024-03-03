using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Setup;
public class RoomSeatEditVm
{
    public string Name { get; set; }
    // public string Position { get; set; }
    public bool IsLastSeat { get; set; }
    // public long RoomId { get; set; }
    // public List<Room> Rooms;
    // public SelectList RoomSelectList()
    // {
    //     return new SelectList(
    //         Rooms,
    //         nameof(Room.Id), //property use for value
    //         nameof(Room.Name), //property use for display
    //         RoomId
    //     );
    // }
}
