

using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Movies;
using SimpleAuth.ViewModels.MovieShow;
using SimpleAuth.ViewModels.Shows;


namespace SimpleAuth.Controllers;
[AllowAnonymous]
public class MovieShowController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly IWebHostEnvironment _webHostEnvironment; //for file uploads

    public MovieShowController(ApplicationDbContext context, INotyfService notification, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _notification = notification;
    }
    public async Task<IActionResult> Index(long Id)
    {
        var vm = new MovieShowVm();
        vm.Movie = await _context.Movies.Where(x => x.Id == Id).Include(x => x.Genres).FirstOrDefaultAsync();
        vm.Shows = await _context.Shows.Where(x => x.MovieId == Id).Include(x => x.Rooms).ToListAsync();

        return View(vm);
    }

    public async Task<IActionResult> MovieShowSeats(long MovieId, long ShowId)
    {
        var vm = new MovieShowSeatsVm();
        vm.Movie = await _context.Movies.Where(x => x.Id == MovieId).Include(x => x.Genres).FirstOrDefaultAsync();

        vm.Show = await _context.Shows.Where(x => x.Id == ShowId).Include(x => x.Rooms).FirstOrDefaultAsync();

        vm.ShowSeats = await _context.ShowSeats.Where(x => x.ShowId == ShowId).ToListAsync();

        List<List<ShowSeat>> showSeats = new List<List<ShowSeat>>();

        var x = new List<ShowSeat>();
        showSeats.Add(x);

        foreach (var item in vm.ShowSeats)
        {
            x.Add(item);
            if (item.IsLastSeatInRow)
            {
                x = new List<ShowSeat>();
                showSeats.Add(x);
            }
        }
        vm.DecoratedShowSeats = showSeats;

        return View(vm);
    }

}


