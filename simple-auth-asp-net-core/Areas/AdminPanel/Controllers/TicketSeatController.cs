
using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Setup;
using SimpleAuth.ViewModels.TicketSeatsAdminPanel;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class TicketSeatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;

    public TicketSeatController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(long Id, TicketSeatsAdminIndexVm vm)
    {
        vm.TicketSeats = await _context.TicketSeats.Where(x => (string.IsNullOrEmpty(vm.SeatName) || x.SeatName.Contains(vm.SeatName)) && x.TicketId == Id).ToListAsync();

        return View(vm);

    }
}
