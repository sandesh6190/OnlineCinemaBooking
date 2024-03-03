
using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.ViewModels.Movies;


namespace SimpleAuth.Controllers;
[AllowAnonymous]
public class MovieListController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly IWebHostEnvironment _webHostEnvironment; //for file uploads

    public MovieListController(ApplicationDbContext context, INotyfService notification, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _notification = notification;
    }
    public async Task<IActionResult> NowShowing(MovieIndexVm vm)
    {

        vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();

        vm.SearchGenres = await _context.Genres.ToListAsync();

        return View(vm);

    }

    public async Task<IActionResult> ComingSoon(MovieIndexVm vm)
    {

        vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon).ToListAsync();

        vm.SearchGenres = await _context.Genres.ToListAsync();

        return View(vm);

    }

}


