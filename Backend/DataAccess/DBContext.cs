using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Backend.DataAccess.DAO_Models;

namespace Backend.DataAccess;

public class DBContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<Table> Tables { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Restriction> Restrictions { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
	
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }
}