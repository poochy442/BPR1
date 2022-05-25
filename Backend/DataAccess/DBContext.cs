using Microsoft.EntityFrameworkCore;
using Backend.Models;

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
	
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>().HasData(new User()
		{
			Id = -1,
			Email = "admin@test.com",
			Password = BCrypt.Net.BCrypt.HashPassword("test1234"),
			Name = "admin",
			PhoneNo = "88888888",
			Role = Role.Admin
		});
	}

}