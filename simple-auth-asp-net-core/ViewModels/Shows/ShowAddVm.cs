using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Constants;
using SimpleAuth.Models;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Shows;
public class ShowAddVm
{
    public long MovieId { get; set; }
    public List<Movie> Movies;//to stored all the movies
    public SelectList MovieSelectList() //to give a select list 
    {
        return new SelectList(
            Movies,//list of genres
            nameof(Movie.Id),//property use for value
            nameof(Movie.Title),//property use for display
            MovieId //where selected movieId stored
        );
    }
    public long RoomId { get; set; }
    public List<Room> Rooms;
    public SelectList RoomSelectList()
    {
        return new SelectList(
            Rooms,//list of rooms
            nameof(Room.Id),//property use for value
            nameof(Room.Name),//property use for display
            RoomId //where selected roomId stored
        );
    }
    public string ShowStatus { get; set; }
    //for selectlist
    public SelectList ShowStatusSelectList()
    {
        return new SelectList(
            ShowStatusConstants.ShowStatusList,
            // nameof(MovieStatusConstants.StatusList),
            // nameof(MovieStatusConstants.StatusList),
            ShowStatus
        );
    }
    public DateTime ShowTime { get; set; }
    public int Cost { get; set; }
}
