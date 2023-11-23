namespace SimpleAuth.Constants;
public class MovieStatusConstants
{
    public const string NowShowing = "Now Showing";
    public const string ComingSoon = "Coming Soon";
    public const string Unavailable = "Unavailable";
    public static List<string> StatusList = new List<string>{
        NowShowing, ComingSoon, Unavailable
    };
}
