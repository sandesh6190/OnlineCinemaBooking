using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.ViewModels.TicketAdminPanel;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class TicketController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly IWebHostEnvironment _webHostEnvironment; //for file uploads

    public TicketController(ApplicationDbContext context, INotyfService notification, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _notification = notification;
    }
    public async Task<IActionResult> Index(TicketAdminIndexVm vm)
    {

        vm.Tickets = await _context.Tickets.Where(x => (vm.SearchByMovieId == null) || (x.Shows.Movies.Id == vm.SearchByMovieId)).Include(x => x.Shows).ThenInclude(x => x.Movies).ToListAsync();


        // vm.SearchGenres = await _context.Genres.ToListAsync();
        // vm.SearchByRoom = await _context.Rooms.ToListAsync();
        vm.SearchByMovies = await _context.Movies.ToListAsync();

        return View(vm);

    }
}
