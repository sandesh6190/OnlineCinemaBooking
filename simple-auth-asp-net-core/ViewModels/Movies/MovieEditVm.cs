using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Constants;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Movies;
public class MovieEditVm
{
    public string Title { get; set; }
    public DateTime ReleasedDate { get; set; }
    public IFormFile? Poster { get; set; }
    public IFormFile? Trailer { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public string Description { get; set; }
    public string MovieStatus { get; set; }
    //for selectlist
    public SelectList StatusSelectList()
    {
        return new SelectList(
            MovieStatusConstants.StatusList,
            // nameof(MovieStatusConstants.StatusList),
            // nameof(MovieStatusConstants.StatusList),
            MovieStatus
        );
    }
    //for data binding
    public long GenreId { get; set; }
    //for displaying GenreList
    public List<Genre> Genres;
    //for selectList
    public SelectList GenreSelectList()
    {
        return new SelectList(
            Genres, //list of genres
            nameof(Genre.Id), //property use for value
            nameof(Genre.Name), //property use for display
            GenreId
        );
    }

    public long LanguageId { get; set; }
    public List<Language> Languages;
    public SelectList LanguageSelectList()
    {
        return new SelectList(
          Languages,
          nameof(Language.Id),
          nameof(Language.Name),
          LanguageId
        );
    }
}
