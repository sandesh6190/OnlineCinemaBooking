namespace SimpleAuth.ViewModels.PublicInterface;
public class RegisterVm
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    //To show error message
    public string ErrorMessage;
}
