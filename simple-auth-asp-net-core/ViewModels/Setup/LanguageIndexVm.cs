using SimpleAuth.Setup;
using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.Setup;
public class LanguageIndexVm
{
    public string Name { get; set; }
    public string Country { get; set; }
    public List<Language> Languages;

}
