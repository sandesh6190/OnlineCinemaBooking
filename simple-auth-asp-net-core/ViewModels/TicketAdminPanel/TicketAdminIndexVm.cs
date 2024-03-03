using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.TicketAdminPanel;
public class TicketAdminIndexVm
{
    //public List<TicketSeat> TicketSeats { get; set; }
    public List<Ticket> Tickets { get; set; }
    public long? SearchByMovieId { get; set; }
    //for displaying GenreList
    public List<Movie> SearchByMovies;
    //for selectList
    public SelectList SearchByMovieSelectList()
    {
        return new SelectList(
            SearchByMovies, //list of genres
            nameof(Movie.Id), //property use for value
            nameof(Movie.Title), //property use for display
            SearchByMovieId
        );
    }
    //public int NoOfSeats { get; set; }

    //public string SearchByRoom { get; set; }
}
