using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Backend.DataAccess.Models;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Roles 
        modelBuilder.Entity<Role>().HasData(new Role()
        {
            Id = 1,
            Name = "CustomerRole",
            Claims = "Customer"
        });

        modelBuilder.Entity<Role>().HasData(new Role()
        {
            Id = 2,
            Name = "RestaurantManagerRole",
            Claims = "RestaurantManager"
        });


        // Address
        modelBuilder.Entity<Address>().HasData(new Address()
        {
            Id = 1,
            PostalCode = "8700",
            City = "Horsens",
            Street = "Borgergade",
            StreetNo = "15",
            Latitude = 55.86155m,
            Longtitude = 9.85451m
        });

        modelBuilder.Entity<Address>().HasData(new Address()
        {
            Id = 2,
            PostalCode = "8700",
            City = "Horsens",
            Street = "Flintebakken",
            StreetNo = "1",
            Latitude = 55.87022m,
            Longtitude = 9.8643m

        });

        modelBuilder.Entity<Address>().HasData(new Address()
        {
            Id = 3,
            PostalCode = "8700",
            City = "Horsens",
            Street = "Borgergade",
            StreetNo = "25",
            Latitude = 55.86142m,
            Longtitude = 9.85619m

        });

        modelBuilder.Entity<Address>().HasData(new Address()
        {
            Id = 4,
            PostalCode = "8700",
            City = "Horsens",
            Street = "Levysgade",
            StreetNo = "2",
            Latitude = 55.8617897m,
            Longtitude = 9.84358882m

        });

        modelBuilder.Entity<Address>().HasData(new Address()
        {
            Id = 5,
            PostalCode = "8700",
            City = "Horsens",
            Street = "Søndergade",
            StreetNo = "15",
            Latitude = 55.862095m,
            Longtitude = 9.8471868m

        });


        // Users
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            Email = "user@email.com",
            Password = "$2a$11$YPB10zErqK1sD2h61D0xkupRJMObqFlaGoXcZ5TcVuIij.oWZlZGy", // = 'password'
            Name = "Steve G",
            PhoneNo = "+4596254585",
            RoleId = 1
        });

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 2,
            Email = "manager@email.com",
            Password = "$2a$11$Y7zhcGWJn.Ym8gp6XmvOe.m09CcAnynFZtV6eL7Hc9Tk9ffMCYWVK", // = 'password'
            Name = "Clive L",
            PhoneNo = "+4532124565",
            RoleId = 2
        });


        // Restaurant
        modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
        {
            Id = 1,
            Name = "Pizza King",
            FoodType = "Pizza",
            StudentDiscount = 5,
            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n}\r\n]",
            TotalScore = 4,
            UserId = 2,
            AddressId = 1
        });

        modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
        {
            Id = 2,
            Name = "Mc Donald's",
            FoodType = "Fast Food",
            StudentDiscount = 7,
            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n}\r\n]",
            TotalScore = 4.4m,
            UserId = 2,
            AddressId = 2
        });

        modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
        {
            Id = 3,
            Name = "Venezia",
            FoodType = "Italian",
            StudentDiscount = 5,
            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"00:00\",\r\n\"Till\": \"00:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n}\r\n]",
            TotalScore = 4.2m,
            UserId = 2,
            AddressId = 3
        });

        modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
        {
            Id = 4,
            Name = "Ming Hao",
            FoodType = "Asian",
            StudentDiscount = 5,
            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"12:30\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"12:00\",\r\n\"Till\": \"21:30\"\r\n}\r\n]",
            TotalScore = 4.0m,
            UserId = 2,
            AddressId = 4
        });

        modelBuilder.Entity<Restaurant>().HasData(new Restaurant()
        {
            Id = 5,
            Name = "GranBar",
            FoodType = "Danish",
            StudentDiscount = 5,
            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"10:00\",\r\n\"Till\": \"21:00\"\r\n}\r\n]",
            TotalScore = 4.2m,
            UserId = 2,
            AddressId = 5
        });


        // Restrictions
        modelBuilder.Entity<Restriction>().HasData(new Restriction()
        {
            Id = 1,
            Age = true,
            Handicap = false
        });

        modelBuilder.Entity<Restriction>().HasData(new Restriction()
        {
            Id = 2,
            Age = false,
            Handicap = true
        });

        modelBuilder.Entity<Restriction>().HasData(new Restriction()
        {
            Id = 3,
            Age = true,
            Handicap = true
        });


        // Tables
        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 1,
            TableNo = 1,
            Seats = 2,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1,
            RestrictionId = 1
        });

        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 2,
            TableNo = 2,
            Seats = 2,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1,
        });

        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 3,
            TableNo = 3,
            Seats = 4,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1,
            RestrictionId = 2
        });

        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 4,
            TableNo = 4,
            Seats = 4,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1,
        });

        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 5,
            TableNo = 5,
            Seats = 6,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1
        });

        modelBuilder.Entity<Table>().HasData(new Table()
        {
            Id = 6,
            TableNo = 6,
            Seats = 6,
            Available = true,
            Deadline = new DateTime(1970, 01, 01, 01, 00, 00),
            RestaurantId = 1,
        });


        // Bookings
        modelBuilder.Entity<Booking>().HasData(new Booking()
        {
            Id = 1,
            Date = new DateTime(2022, 06, 24, 0, 0, 0),
            StartDate = new DateTime(2022, 06, 24, 9, 0, 0),
            EndDate = new DateTime(2022, 06, 24, 10, 0, 0),
            GuestNo = 2,
            RestaurantId = 1,
            TableId = 1,
            UserId = 1
        });

        modelBuilder.Entity<Booking>().HasData(new Booking()
        {
            Id = 2,
            Date = new DateTime(2022, 06, 24, 0, 0, 0),
            StartDate = new DateTime(2022, 06, 24, 12, 0, 0),
            EndDate = new DateTime(2022, 06, 24, 15, 0, 0),
            GuestNo = 2,
            RestaurantId = 1,
            TableId = 1,
            UserId = 1
        });

        modelBuilder.Entity<Booking>().HasData(new Booking()
        {
            Id = 3,
            Date = new DateTime(2022, 06, 24, 0, 0, 0),
            StartDate = new DateTime(2022, 06, 24, 17, 0, 0),
            EndDate = new DateTime(2022, 06, 24, 20, 0, 0),
            GuestNo = 2,
            RestaurantId = 1,
            TableId = 1,
            UserId = 1
        });

        modelBuilder.Entity<Booking>().HasData(new Booking()
        {
            Id = 4,
            Date = new DateTime(2022, 06, 24, 0, 0, 0),
            StartDate = new DateTime(2022, 06, 24, 10, 0, 0),
            EndDate = new DateTime(2022, 06, 24, 12, 0, 0),
            GuestNo = 2,
            RestaurantId = 1,
            TableId = 2,
            UserId = 1
        });

        modelBuilder.Entity<Booking>().HasData(new Booking()
        {
            Id = 5,
            Date = new DateTime(2022, 06, 24, 0, 0, 0),
            StartDate = new DateTime(2022, 06, 24, 15, 0, 0),
            EndDate = new DateTime(2022, 06, 24, 17, 0, 0),
            GuestNo = 2,
            RestaurantId = 1,
            TableId = 2,
            UserId = 1
        });

    }
}