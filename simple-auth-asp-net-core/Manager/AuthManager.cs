using System.Security.Claims;
using System.Transactions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Entity;
using SimpleAuth.Manager.Interfaces;

namespace SimpleAuth.Manager;

public class AuthManager : IAuthManager
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthManager(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == username.ToLower().Trim());
        if (user == null)
        {
            throw new Exception("Invalid username");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new Exception("Username and password do not match");
        }

        var httpContext = _httpContextAccessor.HttpContext;
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString())
        };

        if (user.UserType == UserTypeConstants.Admin)
        {
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync();
    }

    public async Task Register(string name, string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower().Trim());

        if (user != null)
        {
            throw new Exception("Email already existed");
        }
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var NewUser = new User()
        {
            Name = name,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            UserType = UserTypeConstants.NormalUser,
            UserStatus = UserStatusConstants.Active,
            CreatedDate = DateTime.Now
        };
        _context.Users.Add(NewUser);
        await _context.SaveChangesAsync();

        tx.Complete();
    }

}