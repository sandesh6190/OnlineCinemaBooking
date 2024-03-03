using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Constants;
using SimpleAuth.Data;
using SimpleAuth.Models;
using SimpleAuth.Provider.Interfaces;
using SimpleAuth.ViewModels.Tickets;
using Stripe.Checkout; //api reference ko lagi chaiyeko ani matrei sessionCreateOptionsma successurl haru include huncha

namespace SimpleAuth.Controllers;
public class TicketController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly ICurrentUserProvider _currentUser;
    public TicketController(ApplicationDbContext context, INotyfService notification, ICurrentUserProvider currentUser)
    {
        _context = context;
        _notification = notification;
        _currentUser = currentUser;
    }
    [HttpPost]
    public async Task<IActionResult> BookingTicket([FromBody] TicketVm vm)
    {
        try
        {
            if (vm == null)
            {
                return BadRequest("Invalid request data");
            }
            else
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var CurrentUser = await _currentUser.GetCurrentUser();

                var ticket = new Ticket();
                ticket.BookedTime = DateTime.Now;
                ticket.BookedBy = CurrentUser.Name;
                ticket.ShowId = vm.ShowId;

                var show = await _context.Shows.Where(x => x.Id == vm.ShowId).FirstOrDefaultAsync();

                ticket.TotalAmount = show.Cost * vm.ShowSeatVms.Count();
                ticket.PaymentStatus = PaymentStatusConstants.Pending;

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                foreach (var ticketSeatsVm in vm.ShowSeatVms)
                {
                    var ticketSeat = new TicketSeat();
                    ticketSeat.TicketId = ticket.Id;
                    ticketSeat.Tickets = ticket;
                    ticketSeat.ShowSeatId = ticketSeatsVm.ShowSeatId;

                    var showSeat = await _context.ShowSeats.Where(x => x.Id == ticketSeatsVm.ShowSeatId).FirstOrDefaultAsync();

                    ticketSeat.SeatName = showSeat.SeatName;

                    showSeat.SeatStatus = SeatStatusConstants.Reserved;
                    _context.ShowSeats.Update(showSeat);

                    _context.TicketSeats.Add(ticketSeat);

                }
                await _context.SaveChangesAsync();
                // tx.Complete();

                //var CurrentUser = await _currentUser.GetCurrentUser();
                var domain = "http://localhost:5041/";

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"Ticket/TicketBookedSuccessfully?TicketId={ticket.Id}",
                    CancelUrl = domain + $"Ticket/TicketBookFailed?ticketId={ticket.Id}",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    CustomerEmail = CurrentUser.Email
                };

                foreach (var showSeatVm in vm.ShowSeatVms)
                {

                    var ShowSeat = await _context.ShowSeats.Where(x => x.Id == showSeatVm.ShowSeatId).Include(x => x.Shows).FirstOrDefaultAsync();
                    var sessionListItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(ShowSeat.Shows.Cost * 100),
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = ShowSeat.SeatName
                            }
                        },
                        Quantity = 1
                    };
                    options.LineItems.Add(sessionListItem);
                }
                tx.Complete();

                var service = new SessionService();
                Session session = service.Create(options);

                TempData["Session"] = session.Id;

                //These below commented code are implemented if this action isn't called through fetchAPI

                //Response.Headers.Add("Location", session.Url); //form bata submit huda

                //return new StatusCodeResult(303); //form bata submit huda

                return Json(new
                {
                    redir = session.Url,
                    success = true
                });
            }
        }
        catch (Exception e)
        {
            return Json(new
            {
                success = false,
                Message = e.Message
            });
        }


    }


    public async Task<IActionResult> TicketBookedSuccessfully(long? TicketId)
    {
        try
        {
            if (TicketId != null)
            {

                var CurrentUser = await _currentUser.GetCurrentUser();
                var ticketSeats = await _context.TicketSeats.Where(x => x.TicketId == TicketId).Include(x => x.Tickets).Include(x => x.ShowSeats).ToListAsync();

                var Ticket = await _context.Tickets.Where(x => x.Id == TicketId).Include(x => x.Shows).ThenInclude(x => x.Movies).FirstOrDefaultAsync();

                var vm = new TicketBookedSuccessVm();
                vm.TicketId = TicketId;
                vm.MovieName = Ticket.Shows.Movies.Title;
                vm.BookedTime = Ticket.BookedTime;
                vm.BookedBy = Ticket.BookedBy;
                vm.ShowTime = Ticket.Shows.ShowTime;
                vm.PerTicketCost = Ticket.Shows.Cost;

                // vm.TotalCost = Ticket.TotalAmount;

                vm.ShowSeatNames = ticketSeats.Select(x => new ShowSeatName
                {
                    SeatName = x.ShowSeats.SeatName
                }).ToList();


                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


                foreach (var TicketSeat in ticketSeats)
                {
                    TicketSeat.Tickets.PaymentStatus = PaymentStatusConstants.Paid;
                    TicketSeat.Tickets.BookedTime = DateTime.Now;
                    TicketSeat.Tickets.BookedBy = CurrentUser.Email;

                    TicketSeat.ShowSeats.SeatStatus = SeatStatusConstants.Booked;
                    TicketSeat.ShowSeats.BookedByPerson = CurrentUser.Email;

                    _context.TicketSeats.Update(TicketSeat);

                }
                await _context.SaveChangesAsync();
                tx.Complete();
                return View(vm);
            }
            else
            {
                return BadRequest("Ticket ID is required.");
            }

        }
        catch (Exception e)
        {
            _notification.Error(e.Message);
            return RedirectToAction("TicketBookFailed", new
            {
                ticketId = TicketId
            });
        }
    }

    public async Task<IActionResult> TicketBookFailed(long? ticketId)
    {
        if (ticketId != null)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var ticket = await _context.Tickets.Where(x => x.Id == ticketId).FirstOrDefaultAsync();
            _context.Tickets.Remove(ticket);

            var ticketSeats = await _context.TicketSeats.Where(x => x.TicketId == ticketId).Include(x => x.ShowSeats).ToListAsync();

            foreach (var ticketSeat in ticketSeats)
            {
                ticketSeat.ShowSeats.SeatStatus = SeatStatusConstants.Available;
                _context.TicketSeats.Update(ticketSeat);
            }
            await _context.SaveChangesAsync();
            tx.Complete();

            return View();
        }
        else
        {
            _notification.Error("Ticket Booking Canceled");
            return View();
        }

    }
}





