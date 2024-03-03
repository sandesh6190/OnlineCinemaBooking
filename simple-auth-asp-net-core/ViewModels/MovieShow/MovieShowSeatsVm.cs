using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.MovieShow;
public class MovieShowSeatsVm
{
    public Movie Movie { get; set; }
    public Show Show { get; set; }
    public List<ShowSeat> ShowSeats;
    public List<List<ShowSeat>> DecoratedShowSeats;
}
