using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.ViewModels.Movies;


namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class MovieController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly IWebHostEnvironment _webHostEnvironment; //for file uploads

    public MovieController(ApplicationDbContext context, INotyfService notification, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _notification = notification;
    }
    public async Task<IActionResult> Index(MovieIndexVm vm)
    {

        vm.Movies = await _context.Movies.Where(
        x => (string.IsNullOrEmpty(vm.Search) || x.Title.Contains(vm.Search) || x.Director.Contains(vm.Search) || x.Cast.Contains(vm.Search))
        && (vm.SearchGenreId == null || x.GenreId == vm.SearchGenreId)
        && (vm.SearchReleasedDate == null || x.ReleasedDate.Date == vm.SearchReleasedDate.Value.Date) && (vm.SearchStatus == null || x.MovieStatus == vm.SearchStatus)
        ).Include(x => x.Languages).Include(x => x.Genres)
        .ToListAsync();

        vm.SearchGenres = await _context.Genres.ToListAsync();

        return View(vm);

    }

    public async Task<IActionResult> Add()
    {
        var vm = new MovieAddVm();
        vm.Genres = await _context.Genres.ToListAsync(); //view ma add garda genreko list dekhaunalai
        vm.Languages = await _context.Languages.ToListAsync(); //view ma add garda languageko list dekhaunalai
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MovieAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = await _context.Genres.ToListAsync(); //view ma add garda genreko list dekhaunalai
                vm.Languages = await _context.Languages.ToListAsync(); //view ma add garda languageko list dekhaunalai
                _notification.Error("Validation Error.");
                return View(vm);
            }

            if (vm.Poster == null || vm.Trailer == null)
            {
                throw new Exception("No Poster is available.");
            }

            //for Poster
            //for getting extension of a file
            var extension = Path.GetExtension(vm.Poster.FileName);
            //getting random fileName
            var fileName = Guid.NewGuid().ToString() + "." + extension;
            //setting root directory for uploadig file
            var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Poster");
            //setting file path
            var filePath = Path.Combine(rootPath, fileName);
            //FileStream is necessary for it,so creating Variable of FileStream with filePath and copying vm.Poster into stream
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.Poster.CopyToAsync(stream);
            };

            //for Trailer
            //for getting extension
            var TrailerExtension = Path.GetExtension(vm.Trailer.FileName);
            //getting random fileName
            var TrailerFileName = Guid.NewGuid().ToString() + "." + TrailerExtension;
            //setting root directory for uploading file
            var TrailerRootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Trailer");
            //setting file Path
            var TrailerFilePath = Path.Combine(TrailerRootPath, TrailerFileName);
            //creating variable of fileStream with filePath parameter and copying vm.Trailer into filestream variable
            using (var stream = new FileStream(TrailerFilePath, FileMode.Create))
            {
                await vm.Trailer.CopyToAsync(stream);
            }


            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var movie = new Movie();
            movie.Title = vm.Title;
            movie.ReleasedDate = vm.ReleasedDate.Date;
            movie.LanguagesId = vm.LanguageId;
            movie.GenreId = vm.GenreId;
            movie.Director = vm.Director;
            movie.Cast = vm.Cast;
            movie.Description = vm.Description;
            movie.MovieStatus = vm.MovieStatus;
            movie.Poster = fileName; //for views <img src="/Uploads/Poster/@Model.Poster"/>
            movie.Trailer = TrailerFileName;
            movie.DateModified = DateTime.Now;

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Movie Added.");
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var movie = await _context.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (movie == null)
            {
                throw new Exception("No Movie Found.");
            }
            var vm = new MovieEditVm();
            vm.Title = movie.Title;
            vm.ReleasedDate = movie.ReleasedDate.Date;
            vm.LanguageId = movie.LanguagesId;
            vm.GenreId = movie.GenreId;
            vm.Director = movie.Director;
            vm.Cast = movie.Cast;
            vm.Description = movie.Description;
            vm.MovieStatus = movie.MovieStatus;
            // vm.Poster = movie.Poster;
            // vm.Trailer = movie.Trailer;

            vm.Genres = await _context.Genres.ToListAsync();
            vm.Languages = await _context.Languages.ToListAsync();

            return View(vm);
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, MovieEditVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = await _context.Genres.ToListAsync(); //view ma add garda genreko list dekhaunalai
                vm.Languages = await _context.Languages.ToListAsync(); //view ma add garda languageko list dekhaunalai
                _notification.Error("Validation Error.");
                return View(vm);
            }
            var movie = await _context.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new Exception("No Movie Found.");
            }

            string? posterFileName = null;
            string? trailerFileName = null;

            if (vm.Poster != null)
            {
                var extension = Path.GetExtension(vm.Poster.FileName);
                //getting random fileName
                posterFileName = Guid.NewGuid().ToString() + "." + extension;
                //setting root directory for uploadig file
                var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Poster");
                //setting file path
                var filePath = Path.Combine(rootPath, posterFileName);
                //FileStream is necessary for it,so creating Variable of FileStream with filePath and copying vm.Poster into stream
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Poster.CopyToAsync(stream);
                };
            }
            if (vm.Trailer != null)
            {
                //for getting extension
                var TrailerExtension = Path.GetExtension(vm.Trailer.FileName);
                //getting random fileName
                trailerFileName = Guid.NewGuid().ToString() + "." + TrailerExtension;
                //setting root directory for uploading file
                var TrailerRootPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Trailer");
                //setting file Path
                var TrailerFilePath = Path.Combine(TrailerRootPath, trailerFileName);
                //creating variable of fileStream with filePath parameter and copying vm.Trailer into filestream variable
                using (var stream = new FileStream(TrailerFilePath, FileMode.Create))
                {
                    await vm.Trailer.CopyToAsync(stream);
                }
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            movie.Title = vm.Title;
            movie.ReleasedDate = vm.ReleasedDate.Date;
            movie.LanguagesId = vm.LanguageId;
            movie.GenreId = vm.GenreId;
            movie.Director = vm.Director;
            movie.Cast = vm.Cast;
            movie.Description = vm.Description;
            movie.MovieStatus = vm.MovieStatus;

            if (!string.IsNullOrEmpty(trailerFileName))
            {
                movie.Trailer = trailerFileName;
            }
            if (!string.IsNullOrEmpty(posterFileName))
            {
                movie.Poster = posterFileName;
            }

            movie.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Movie Edited.");
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var movie = await _context.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new Exception("No Movie Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Movie Deleted.");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

}


