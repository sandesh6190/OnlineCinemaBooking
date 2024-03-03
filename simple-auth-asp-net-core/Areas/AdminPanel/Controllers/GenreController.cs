using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleAuth.Data;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Setup;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class GenreController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;

    public GenreController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }
    public async Task<IActionResult> Index(GenreIndexVm vm)
    {
        vm.Genres = await _context.Genres.Where(x => string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name))
            //.Include(x => x.Employees) // To Get Employee data automatically
            .ToListAsync();
        return View(vm);

        //Second Option
        // vm.DisplayData = await _context.Products
        //     .Where(x =>
        //         string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
        //     )
        //     .Select(x => new ProductDisplayVm()
        //     {
        //         Id = x.Id,
        //         Name = x.Name
        //     })
        //     .ToListAsync();
    }

    public IActionResult Add()
    {
        var vm = new GenreAddVm();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(GenreAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _notification.Error("Validation error");
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var genre = new Genre();
                genre.Name = vm.Name;
                genre.Description = vm.Description;
                genre.DateModified = DateTime.Now;

                //mark an object to be inserted
                _context.Genres.Add(genre);

                // Send changes to database
                await _context.SaveChangesAsync();
                // Complete the transaction
                tx.Complete();

                _notification.Success("Genre Added.");

            }
            // return RedirectToAction("Index", "Genre", new { area = "AdminPanel" });
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error("Genre couldn't Added.");
            return RedirectToAction("Index", "Genre");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var genre = await _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();

        try
        {
            if (genre == null)
            {
                //_notification.Error("No Genre found.");
                throw new Exception("No Genre found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            //Mapping object
            _context.Genres.Remove(genre);
            //Saving on database
            await _context.SaveChangesAsync();

            tx.Complete();

            _notification.Success("Genre Deleted.");
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
            var genre = await _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (genre == null)
            {
                throw new Exception("No Genre Found.");
            }
            var vm = new GenreEditVm();
            vm.Name = genre.Name;
            vm.Description = genre.Description;

            return View(vm);
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, GenreEditVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var genre = await _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (genre == null)
            {
                throw new Exception("No Genre Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            genre.Name = vm.Name;
            genre.Description = vm.Description;
            genre.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Genre Edited.");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }
}
