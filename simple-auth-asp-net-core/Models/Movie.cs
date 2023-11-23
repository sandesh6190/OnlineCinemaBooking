using System.Runtime.Intrinsics.X86;
using SimpleAuth.Constants;
using SimpleAuth.Setup;

namespace SimpleAuth.Models;
public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleasedDate { get; set; }
    public long LanguagesId { get; set; }
    public virtual Language Languages { get; set; }
    public long GenreId { get; set; }
    public virtual Genre Genres { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public string Description { get; set; }
    public string MovieStatus { get; set; } = MovieStatusConstants.ComingSoon;

    //Poster and Trailer and releasedYear
    public string Poster { get; set; }
    public string Trailer { get; set; }
    public DateTime DateModified { get; set; }
}

