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

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }

    internal Task SaveChanges(Genre genre)
    {
        throw new NotImplementedException();
    }
}