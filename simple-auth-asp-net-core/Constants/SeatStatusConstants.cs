namespace SimpleAuth.Constants;
public class SeatStatusConstants
{
    public const string Available = "Available";
    public const string Reserved = "Reserved";
    public const string Booked = "Booked";

    public static List<string> SeatStatusList = new List<string>{
        Available, Reserved, Booked
    };
}
