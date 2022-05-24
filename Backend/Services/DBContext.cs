using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

using Backend.Models;

namespace Backend.Services;

public class DBContext : DbContext
{
	public DbSet<Restaurant> Restaurants { get; set; } = null!;
	public DbSet<Table> Tables { get; set; } = null!;
	public DbSet<Booking> Bookings { get; set; } = null!;
	public DbSet<Rating> Ratings { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;

	public DBContext(DbContextOptions<DBContext> options)
		: base(options)
	{
	}
}