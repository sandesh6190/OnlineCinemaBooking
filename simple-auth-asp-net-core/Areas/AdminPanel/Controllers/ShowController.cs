using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.Setup;
using SimpleAuth.ViewModels.Shows;

namespace SimpleAuth.AdminPanel.Controllers;
[Area("AdminPanel")]
[Authorize(Roles = "Admin")]
public class ShowController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    public ShowController(ApplicationDbContext context, INotyfService notification)
    {
        _context = context;
        _notification = notification;
    }

    public async Task<IActionResult> Index(ShowIndexVm vm)
    {
        vm.Shows = await _context.Shows.Where(x => (vm.SearchMovieId == null || x.MovieId == vm.SearchMovieId) && (vm.SearchRoomId == null || x.RoomId == vm.SearchRoomId) && (vm.SearchShowStatus == null || x.ShowStatus == vm.SearchShowStatus)).Include(x => x.Movies).Include(x => x.Rooms).ToListAsync();

        //  && (vm.SearchShowTime == null || x.ShowTime.Date == vm.SearchShowTime.Value.Date)
        vm.SearchMovies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();//to list all movies
        vm.SearchRooms = await _context.Rooms.ToListAsync();//to list all rooms
        return View(vm);
    }

    public async Task<IActionResult> Add()
    {
        var vm = new ShowAddVm();
        vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();
        vm.Rooms = await _context.Rooms.ToListAsync();
        return View(vm);

    }
    [HttpPost]
    public async Task<IActionResult> Add(ShowAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();
                vm.Rooms = await _context.Rooms.ToListAsync();

                _notification.Error("Valildation Error.");
                return View(vm);
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var show = new Show();
            show.MovieId = vm.MovieId;
            show.RoomId = vm.RoomId;
            show.ShowTime = vm.ShowTime;
            show.ShowStatus = vm.ShowStatus;
            show.Cost = vm.Cost;
            show.DateModified = DateTime.Now;

            _context.Shows.Add(show);
            await _context.SaveChangesAsync();


            var roomSeats = await _context.RoomSeats.Where(x => vm.RoomId == x.RoomId).ToListAsync();

            foreach (var item in roomSeats)
            {
                var showSeat = new ShowSeat();
                showSeat.ShowId = show.Id;
                showSeat.Position = item.Position;
                showSeat.SeatName = item.Name;
                showSeat.IsLastSeatInRow = item.IsLastSeat;
                showSeat.SeatStatus = SeatStatusConstants.Available;
                showSeat.BookedByPerson = "";

                _context.ShowSeats.Add(showSeat);
                await _context.SaveChangesAsync();
            }

            tx.Complete();
            _notification.Success("Show Added.");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> Edit(long Id)
    {
        try
        {
            var show = await _context.Shows.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (show == null)
            {
                throw new Exception("No Show Found.");
            }
            var vm = new ShowEditVm();
            vm.MovieId = show.MovieId;
            vm.RoomId = show.RoomId;
            vm.ShowTime = show.ShowTime;
            vm.ShowStatus = show.ShowStatus;
            vm.Cost = show.Cost;

            vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();
            vm.Rooms = await _context.Rooms.ToListAsync();

            return View(vm);
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long Id, ShowEditVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vm.Movies = await _context.Movies.Where(x => x.MovieStatus == MovieStatusConstants.ComingSoon || x.MovieStatus == MovieStatusConstants.NowShowing).ToListAsync();
                vm.Rooms = await _context.Rooms.ToListAsync();
                _notification.Error("Validation Error.");
                return View(vm);
            }
            var show = await _context.Shows.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (show == null)
            {
                throw new Exception("No Show Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            show.MovieId = vm.MovieId;
            show.RoomId = vm.RoomId;
            show.ShowTime = vm.ShowTime;
            show.ShowStatus = vm.ShowStatus;
            show.Cost = vm.Cost;
            show.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Show Edited.");
            return RedirectToAction("Index");
        }

        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long Id)
    {
        try
        {
            var show = await _context.Shows.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (show == null)
            {
                throw new Exception("No Show Found.");
            }

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            tx.Complete();
            _notification.Success("Show Deleted.");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("Index");
        }
    }
}
