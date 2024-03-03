namespace SimpleAuth.ViewModels.PublicInterface;

public class LoginVm
{
    public string Username { get; set; }
    public string Password { get; set; }

    // To show error message
    public string ErrorMessage;
}