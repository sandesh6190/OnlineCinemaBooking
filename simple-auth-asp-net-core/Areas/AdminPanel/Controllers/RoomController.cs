using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels;
using SimpleAuth.ViewModels.Setup;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class RoomController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;

    public RoomController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(RoomIndexVm vm)
    {
        vm.Rooms = await _context.Rooms.Where(x => string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)).ToListAsync();
        return View(vm);
    }

    public IActionResult Add()
    {
        var vm = new RoomAddVm();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(RoomAddVm vm)
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

                var room = new Room();
                room.Name = vm.Name;
                room.TotalRows = vm.TotalRows;
                room.TotalColumns = vm.TotalColumns;
                room.DateModified = DateTime.Now;

                //mark an object to be inserted
                _context.Rooms.Add(room);
                // Send changes to database
                await _context.SaveChangesAsync();


                char ch = 'A';
                int a = 1;
                for (int i = 1; i <= vm.TotalRows; i++)
                {
                    for (int j = 1; j <= vm.TotalColumns; j++)
                    {
                        var roomSeat = new RoomSeat();
                        roomSeat.Position = a++;
                        roomSeat.Name = string.Concat(ch, j);
                        roomSeat.IsLastSeat = false;
                        if (j == vm.TotalColumns)
                        {
                            roomSeat.IsLastSeat = true;
                        }

                        roomSeat.RoomId = room.Id;
                        _context.RoomSeats.Add(roomSeat);
                        await _context.SaveChangesAsync();
                    }
                    ch++;
                }

                // Complete the transaction
                tx.Complete();

                _notification.Success("Room Added.");
            }

            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error("Room couldn't Added.");
            return RedirectToAction("Index", "Room");
        }
    }

    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var room = await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new Exception("No Room Found.");
            }
            var vm = new RoomEditVm();
            vm.Name = room.Name;
            vm.TotalRows = room.TotalRows;
            vm.TotalColumns = room.TotalColumns;


            return View(vm);
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, RoomEditVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var room = await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new Exception("No Room Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            room.Name = vm.Name;
            room.TotalRows = vm.TotalRows;
            room.TotalColumns = vm.TotalColumns;
            room.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            var roomSeats = await _context.RoomSeats.Where(x => x.RoomId == id).ToListAsync();
            //Mapping object
            _context.RoomSeats.RemoveRange(roomSeats); //RemoveRange is used for removing list of datas from db table
            //Saving on database
            await _context.SaveChangesAsync();

            char ch = 'A';
            int a = 1;
            for (int i = 1; i <= vm.TotalRows; i++)
            {
                for (int j = 1; j <= vm.TotalColumns; j++)
                {
                    var roomSeat = new RoomSeat();
                    roomSeat.Position = a++;
                    roomSeat.Name = string.Concat(ch, j);
                    roomSeat.IsLastSeat = false;
                    if (j == vm.TotalColumns)
                    {
                        roomSeat.IsLastSeat = true;
                    }

                    roomSeat.RoomId = room.Id;
                    _context.RoomSeats.Update(roomSeat);
                    await _context.SaveChangesAsync();
                }
                ch++;
            }

            tx.Complete();
            _notification.Success("Room Edited.");
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
        var room = await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();

        try
        {
            if (room == null)
            {
                //_notification.Error("No Genre found.");
                throw new Exception("No Room found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            //Mapping object
            _context.Rooms.Remove(room);
            //Saving on database
            await _context.SaveChangesAsync();

            tx.Complete();

            _notification.Success("Room Deleted.");
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }
}
