using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Data;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Setup;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class RoomSeatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;

    public RoomSeatController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(long Id, RoomSeatIndexVm vm)
    {
        vm.RoomSeats = await _context.RoomSeats.Where(x => (string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)) && x.RoomId == Id).ToListAsync();

        return View(vm);

    }

    // [HttpPost]
    // public async Task<IActionResult> Delete(long id)
    // {
    //     var RoomSeat = await _context.RoomSeats.Where(x => x.Id == id).FirstOrDefaultAsync();

    //     try
    //     {
    //         if (RoomSeat == null)
    //         {
    //             //_notification.Error("No RoomSeat found.");
    //             throw new Exception("No RoomSeat found.");
    //         }

    //         using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    //         //Mapping object
    //         _context.RoomSeats.Remove(RoomSeat);
    //         //Saving on database
    //         await _context.SaveChangesAsync();

    //         tx.Complete();

    //         _notification.Success("RoomSeat Deleted.");
    //         return RedirectToAction("Index");
    //     }

    //     catch (Exception e)
    //     {
    //         _notification.Error(e.Message);
    //         return RedirectToAction("Index");
    //     }
    // }

    // public async Task<IActionResult> Edit(long id)
    // {
    //     try
    //     {
    //         var RoomSeat = await _context.RoomSeats.Where(x => x.Id == id).FirstOrDefaultAsync();
    //         if (RoomSeat == null)
    //         {
    //             throw new Exception("No RoomSeat Found.");
    //         }
    //         var vm = new RoomSeatEditVm();

    //         vm.Name = RoomSeat.Name;
    //         // vm.Position = RoomSeat.Position;
    //         vm.IsLastSeat = RoomSeat.IsLastSeat;

    //         return View(vm);
    //     }

    //     catch (Exception e)
    //     {
    //         _notification.Error(e.Message);
    //         return RedirectToAction("Index");
    //     }
    // }

    // [HttpPost]
    // public async Task<IActionResult> Edit(long id, RoomSeatEditVm vm)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //         {
    //             return View(vm);
    //         }

    //         var RoomSeat = await _context.RoomSeats.Where(x => x.Id == id).FirstOrDefaultAsync();
    //         if (RoomSeat == null)
    //         {
    //             throw new Exception("No RoomSeat Found.");
    //         }

    //         using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

    //         RoomSeat.Name = vm.Name;

    //         RoomSeat.IsLastSeat = vm.IsLastSeat;


    //         await _context.SaveChangesAsync();

    //         tx.Complete();
    //         _notification.Success("RoomSeat Edited.");
    //         return RedirectToAction("Index");
    //     }
    //     catch (Exception e)
    //     {
    //         _notification.Error(e.Message);
    //         return RedirectToAction("Index");
    //     }
    // }
}
