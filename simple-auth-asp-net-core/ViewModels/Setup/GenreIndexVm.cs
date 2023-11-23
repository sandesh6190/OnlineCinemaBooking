using SimpleAuth.Models;
using SimpleAuth.Setup;

namespace SimpleAuth.ViewModels.Setup;
public class GenreIndexVm
{
    public string Name { get; set; }

    //to show the list of genre
    public List<Genre> Genres;
}
