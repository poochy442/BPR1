﻿// <auto-generated />
using System;
using Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220527204041_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Backend.DataAccess.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Location")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Street")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StreetNo")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Horsens",
                            Location = "longtitude: 1025623; latitude: 1025623",
                            PostalCode = "8700",
                            Street = "Borgergade",
                            StreetNo = "15"
                        },
                        new
                        {
                            Id = 2,
                            City = "Horsens",
                            Location = "longtitude: 1025623; latitude: 1025623",
                            PostalCode = "8700",
                            Street = "Flintebakken",
                            StreetNo = "1"
                        },
                        new
                        {
                            Id = 3,
                            City = "Horsens",
                            Location = "longtitude: 1025623; latitude: 1025623",
                            PostalCode = "8700",
                            Street = "Borgergade",
                            StreetNo = "25"
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GuestNo")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestNo = 2,
                            RestaurantId = 1,
                            StartDate = new DateTime(2022, 6, 24, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            TableId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestNo = 2,
                            RestaurantId = 1,
                            StartDate = new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TableId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2022, 6, 24, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestNo = 2,
                            RestaurantId = 1,
                            StartDate = new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            TableId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestNo = 2,
                            RestaurantId = 1,
                            StartDate = new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            TableId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestNo = 2,
                            RestaurantId = 1,
                            StartDate = new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TableId = 2,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("FoodType")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("StudentDiscount")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalScore")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("varchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            FoodType = "Pizza",
                            Name = "Pizza King",
                            StudentDiscount = 5,
                            TotalScore = 4m,
                            UserId = 2,
                            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n}\r\n]"
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 2,
                            FoodType = "Fast Food",
                            Name = "Mc Donald's",
                            StudentDiscount = 7,
                            TotalScore = 4.4m,
                            UserId = 2,
                            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n}\r\n]"
                        },
                        new
                        {
                            Id = 3,
                            AddressId = 3,
                            FoodType = "Italian",
                            Name = "Venezia",
                            StudentDiscount = 5,
                            TotalScore = 4.2m,
                            UserId = 2,
                            WorkingHours = "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"00:00\",\r\n\"Till\": \"00:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n}\r\n]"
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("Handicap")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Restrictions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 20,
                            Handicap = false
                        },
                        new
                        {
                            Id = 2,
                            Age = 60,
                            Handicap = false
                        },
                        new
                        {
                            Id = 3,
                            Age = 0,
                            Handicap = true
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Claims")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Claims = "Customer",
                            Name = "CustomerRole"
                        },
                        new
                        {
                            Id = 2,
                            Claims = "RestaurantManager",
                            Name = "RestaurantManagerRole"
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("BookingTimes")
                        .HasColumnType("varchar(400)");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int?>("RestrictionId")
                        .HasColumnType("int");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<int>("TableNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("RestrictionId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            RestrictionId = 3,
                            Seats = 2,
                            TableNo = 1
                        },
                        new
                        {
                            Id = 2,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            Seats = 2,
                            TableNo = 2
                        },
                        new
                        {
                            Id = 3,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            RestrictionId = 2,
                            Seats = 4,
                            TableNo = 3
                        },
                        new
                        {
                            Id = 4,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            Seats = 4,
                            TableNo = 4
                        },
                        new
                        {
                            Id = 5,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            RestrictionId = 1,
                            Seats = 6,
                            TableNo = 5
                        },
                        new
                        {
                            Id = 6,
                            Available = true,
                            Deadline = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            Seats = 6,
                            TableNo = 6
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user@email.com",
                            Name = "Steve G",
                            Password = "$2a$11$YPB10zErqK1sD2h61D0xkupRJMObqFlaGoXcZ5TcVuIij.oWZlZGy",
                            PhoneNo = "+4596254585",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "manager@email.com",
                            Name = "Clive L",
                            Password = "$2a$11$Y7zhcGWJn.Ym8gp6XmvOe.m09CcAnynFZtV6eL7Hc9Tk9ffMCYWVK",
                            PhoneNo = "+4532124565",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Booking", b =>
                {
                    b.HasOne("Backend.DataAccess.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");

                    b.HasOne("Backend.DataAccess.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId");

                    b.HasOne("Backend.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Rating", b =>
                {
                    b.HasOne("Backend.DataAccess.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");

                    b.HasOne("Backend.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Restaurant", b =>
                {
                    b.HasOne("Backend.DataAccess.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Backend.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Table", b =>
                {
                    b.HasOne("Backend.DataAccess.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");

                    b.HasOne("Backend.DataAccess.Models.Restriction", "Restriction")
                        .WithMany()
                        .HasForeignKey("RestrictionId");

                    b.Navigation("Restaurant");

                    b.Navigation("Restriction");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.User", b =>
                {
                    b.HasOne("Backend.DataAccess.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Backend.DataAccess.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}