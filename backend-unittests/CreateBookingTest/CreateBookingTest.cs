using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Backend.Controllers;
using Backend.DataAccess;
using Backend.Helpers;
using Backend.BusinessLogic;
using Backend.Helpers.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;

public class CreateBookingTest : IDisposable
{
    private readonly DBContext _dbContext;
    private readonly BookingBL _bookingtBL;
    private readonly BusinessLogic _businessLogic;

    public CreateBookingTest()
    {
        // set db
        // options for db
        var dbOption = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;")
            .Options;
        _dbContext = new DBContext(dbOption);

        // set bookingBL
        _bookingtBL = new BookingBL(_dbContext);

        // set business logic
        _businessLogic = new BusinessLogic(null, null, _bookingtBL, null);
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async void CreateBooking_ValidBooking_True()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.CreateBooking(new CreateBookingRequest()
        {
            Date = new DateTime(2022, 06, 24, 00, 00, 00),
            StartDate = new DateTime(2022, 06, 24, 09, 00, 00),
            EndDate = new DateTime(2022, 06, 24, 10, 00, 00),
            GuestNo = 3,
            RestaurantId = 1,
            TableId = 3,
            UserId = 1
        });

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async void CreateBooking_BookingExists_False()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.CreateBooking(new CreateBookingRequest()
        {
            Date = new DateTime(2022, 06, 24, 00, 00, 00),
            StartDate = new DateTime(2022, 06, 24, 09, 00, 00),
            EndDate = new DateTime(2022, 06, 24, 10, 00, 00),
            GuestNo = 3,
            RestaurantId = 1,
            TableId = 3,
            UserId = 1
        });

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void CreateBooking_InvalidUserId_False()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.CreateBooking(new CreateBookingRequest()
        {
            Date = new DateTime(2022, 06, 24, 00, 00, 00),
            StartDate = new DateTime(2022, 06, 24, 10, 00, 00),
            EndDate = new DateTime(2022, 06, 24, 11, 00, 00),
            GuestNo = 3,
            RestaurantId = 1,
            TableId = 3,
            UserId = 4
        });

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void CreateBooking_InvalidTable_False()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.CreateBooking(new CreateBookingRequest()
        {
            Date = new DateTime(2022, 06, 24, 00, 00, 00),
            StartDate = new DateTime(2022, 06, 24, 10, 00, 00),
            EndDate = new DateTime(2022, 06, 24, 11, 00, 00),
            GuestNo = 3,
            RestaurantId = 1,
            TableId = 8,
            UserId = 1
        });

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    
}