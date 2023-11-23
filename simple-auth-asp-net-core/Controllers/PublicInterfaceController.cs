using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Data;
using SimpleAuth.Manager.Interfaces;
using SimpleAuth.ViewModels.Auth;

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
            return View(vm);
        }

        try
        {
            await _authManager.Login(vm.Username, vm.Password);
            _notifyService.Success("LoggedIn Successful");
            return RedirectToAction("List", "PublicInterface");

        }
        catch (Exception e)
        {
            vm.ErrorMessage = e.Message;
            return View(vm);
        }
    }

    public IActionResult Register()
    {
        var vm = new RegisterVM();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
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

    public IActionResult Index()
    {

        return View();
    }
    public IActionResult List()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
}
