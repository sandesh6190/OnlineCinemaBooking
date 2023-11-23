using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Setup;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
public class LanguageController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    public LanguageController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(LanguageIndexVm vm)
    {
        vm.Languages = await _context.Languages.Where(x => string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name))
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
        var vm = new LanguageAddVm();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LanguageAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _notification.Error("Validation error");
                return View(vm);
            }

            using (var tx = new TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
            {

                var language = new Language();
                language.Name = vm.Name;
                language.Country = vm.Country;
                language.DateModified = DateTime.Now;

                //mark an object to be inserted
                _context.Languages.Add(language);

                // Send changes to database
                await _context.SaveChangesAsync();
                // Complete the transaction
                tx.Complete();

                _notification.Success("Language Added.");

            }
            // return RedirectToAction("Index", "Genre", new { area = "AdminPanel" });
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error("Language couldn't Added.");
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long id) //yo id viewko asp-route-id ko id sanga variableko name milnu parcha
    {
        try
        {
            var language = await _context.Languages.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("Language isn't available.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //mapping object
            _context.Languages.Remove(language);
            //saving on database
            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Language Deleted.");
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
            var language = await _context.Languages.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("Language couldn't found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var vm = new LanguageEditVm();
            vm.Name = language.Name;
            vm.Country = language.Country;

            return View(vm);
        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, LanguageEditVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _notification.Error("Validation Error.");
                return View(vm);
            }
            var language = await _context.Languages.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (language == null)
            {
                throw new Exception("No Language Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            language.Name = vm.Name;
            language.Country = vm.Country;
            language.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();
            tx.Complete();
            _notification.Success("Language Edited.");
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }
}
