using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Constants;
using SimpleAuth.Models;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Shows;
public class ShowIndexVm
{
    public List<Show> Shows;

    public long? SearchMovieId { get; set; }
    public List<Movie> SearchMovies;//to stored all the SearchMovies
    public SelectList SearchMovieSelectList() //to give a select list 
    {
        return new SelectList(
            SearchMovies,//list of genres
            nameof(Movie.Id),//property use for value
            nameof(Movie.Title),//property use for display
            SearchMovieId //where selected SearchMovieId stored
        );
    }
    public long? SearchRoomId { get; set; }
    public List<Room> SearchRooms;
    public SelectList SearchRoomSelectList()
    {
        return new SelectList(
            SearchRooms,//list of SearchRooms
            nameof(Room.Id),//property use for value
            nameof(Room.Name),//property use for display
            SearchRoomId //where selected SearchRoomId stored
        );
    }

    public string SearchShowStatus { get; set; }
    //for selectlist
    public SelectList SearchShowStatusSelectList()
    {
        return new SelectList(
            ShowStatusConstants.ShowStatusList,
            // nameof(MovieStatusConstants.StatusList),
            // nameof(MovieStatusConstants.StatusList),
            SearchShowStatus
        );
    }
    public DateTime? SearchShowTime { get; set; }


}
