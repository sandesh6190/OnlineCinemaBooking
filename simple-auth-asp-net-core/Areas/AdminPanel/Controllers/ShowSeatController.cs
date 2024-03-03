
using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Setup;
using SimpleAuth.ViewModels.ShowSeats;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class ShowSeatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;

    public ShowSeatController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(long Id, ShowSeatIndexVm vm)
    {
        vm.ShowSeats = await _context.ShowSeats.Where(x => (string.IsNullOrEmpty(vm.SeatName) || x.SeatName.Contains(vm.SeatName)) && (vm.SearchByShowSeatStatus == x.SeatStatus || vm.SearchByShowSeatStatus == null) && x.ShowId == Id).ToListAsync();

        return View(vm);

    }
}