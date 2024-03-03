using Microsoft.EntityFrameworkCore;
using SimpleAuth.Entity;
using SimpleAuth.Models;
using SimpleAuth.Setup;

namespace SimpleAuth.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {

    //      modelBuilder.Entity<TicketSeat>().HasOne(x=> x.ShowSeats).
    // }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomSeat> RoomSeats { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<ShowSeat> ShowSeats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketSeat> TicketSeats { get; set; }
}