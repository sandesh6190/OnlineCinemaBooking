using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Constants;
using SimpleAuth.Models;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Movies;
public class MovieIndexVm
{
    public string Search { get; set; }
    public DateTime? SearchReleasedDate { get; set; }
    //yo chai constant listko lagi
    //for data binding
    public string SearchStatus { get; set; }
    //for selectlist
    public SelectList SearchStatusSelectList()
    {
        return new SelectList(
            MovieStatusConstants.StatusList,
            // nameof(MovieStatusConstants.StatusList),
            // nameof(MovieStatusConstants.StatusList),
            SearchStatus
        );
    }
    //for data binding
    public long? SearchGenreId { get; set; }
    //for displaying GenreList
    public List<Genre> SearchGenres;
    //for selectList
    public SelectList SearchGenreSelectList()
    {
        return new SelectList(
            SearchGenres, //list of genres
            nameof(Genre.Id), //property use for value
            nameof(Genre.Name), //property use for display
            SearchGenreId
        );
    }



    public List<Movie> Movies;
}
