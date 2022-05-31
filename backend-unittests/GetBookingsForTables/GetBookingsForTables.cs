using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Backend.Controllers;
using Backend.DataAccess;
using Backend.Helpers;
using Backend.BusinessLogic;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;

public class GetBookingsForTables : IDisposable
{
    private readonly DBContext _dbContext;
    private readonly BookingBL _bookingtBL;
    private readonly BusinessLogic _businessLogic;

    public GetBookingsForTables()
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
    public async void GetBookingsForTables_True()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.GetBookingsForTables(1);

        // Assert
        response.Should().BeOfType<ActionResult<GetTableBookingsResponse>>()
        .Which.Result.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody = response.Result as OkObjectResult;
        Assert.NotNull(responseBody.Value);

        var tableBookings = responseBody.Value.As<GetTableBookingsResponse>().TableBookings;
        Assert.NotNull(tableBookings);
        Assert.NotEmpty(tableBookings);
        Assert.True(tableBookings.Count >= 2);
    }

    [Fact]
    public async void GetBookingsForTables_InvalidRestaurant_False()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.GetBookingsForTables(10);

        // // Assert
        response.Should().BeOfType<ActionResult<GetTableBookingsResponse>>()
        .Which.Result.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetBookingsForTables_NoBookingsForTables_False()
    {
        // Arrange
        var bookingController = new BookingController(_businessLogic);

        // Act
        var response = await bookingController.GetBookingsForTables(2);

        // // Assert
        response.Should().BeOfType<ActionResult<GetTableBookingsResponse>>()
        .Which.Result.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }
}