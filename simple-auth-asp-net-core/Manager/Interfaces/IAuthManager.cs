namespace SimpleAuth.Manager.Interfaces;

public interface IAuthManager
{
    Task Login(string username, string password);
    Task Logout();
    Task Register(string name, string email, string password);
}