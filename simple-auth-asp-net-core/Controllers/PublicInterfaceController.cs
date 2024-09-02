using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Manager.Interfaces;
using SimpleAuth.ViewModels.Movies;
using SimpleAuth.ViewModels.PublicInterface;

namespace SimpleAuth.Controllers;
[AllowAnonymous]
public class PublicInterfaceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthManager _authManager;
    private readonly INotyfService _notifyService;

    public PublicInterfaceController(ApplicationDbContext context, IAuthManager authManager, INotyfService notifyService)
    {
        _context = context;
        _authManager = authManager;
        _notifyService = notifyService;
    }

    public IActionResult Login()
    {
        var vm = new LoginVm();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        if (!ModelState.IsValid)
        {
            _notifyService.Warning("Invalid Input.");
            return View(vm);
        }

        try
        {
            await _authManager.Login(vm.Username, vm.Password);
            _notifyService.Success("LoggedIn Successful");
            return RedirectToAction("Index", "PublicInterface");

        }
        catch (Exception e)
        {
            vm.ErrorMessage = e.Message;
            return View(vm);
        }
    }

    public IActionResult Register()
    {
        var vm = new RegisterVm();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        try
        {
            await _authManager.Register(vm.Name, vm.Email, vm.Password);
            _notifyService.Success("Registration Successful");
            return RedirectToAction("Index", "PublicInterface");
        }

        catch (Exception e)
        {
            vm.ErrorMessage = e.Message;
            return View(vm);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authManager.Logout();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index(MovieIndexVm vm)
    {

        vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();

        return View(vm);

    }
    public IActionResult Contact()
    {
        return View();
    }
}
