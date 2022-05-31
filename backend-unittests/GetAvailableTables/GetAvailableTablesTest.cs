using Xunit;
using FluentAssertions;
using Backend.DataAccess;
using Backend.BusinessLogic;
using Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;

public class GetAvailableTablesTest : IDisposable
{
    private readonly DBContext _dbContext;
    private readonly TableBL _tableBL;
    private readonly BusinessLogic _businessLogic;

    public GetAvailableTablesTest()
    {

        // set db
        // options for db
        var dbOption = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;")
            .Options;
        _dbContext = new DBContext(dbOption);

        // set tableBL
        _tableBL = new TableBL(_dbContext);

        // set business logic
        _businessLogic = new BusinessLogic(null, _tableBL, null, null);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async void GetAvailableTables_ValidBooking_True()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,25,10,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async void GetAvailableTables_ValidRestaurant_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            0,
            2,
            new DateTime(2022,06,25,10,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetAvailableTables_InvalidBookingTime_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,25,09,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetAvailableTables_InvalidTables_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            2,
            2,
            new DateTime(2022,06,25,09,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetAvailableTables_InvalidGuests_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            7,
            new DateTime(2022,06,25,09,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void GetAvailableTables_OverlappingBooking_OneTable_ReplicaBooking_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,24,09,0,0),
            new DateTime(2022,06,24,10,0,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.AvailableTablesResponse;
        Assert.NotNull(responseBody);

        Assert.NotEmpty(responseBody.AvailableTables);

        var table1 = responseBody.AvailableTables.Where(t => t.TableId == 1).FirstOrDefault();
        Assert.NotNull(table1);

        Assert.False(table1.Available);

    }

    [Fact]
    public async void GetAvailableTables_OverlappingBooking_TwoTables_RightOutBooking_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,24,09,30,0),
            new DateTime(2022,06,24,10,30,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.AvailableTablesResponse;
        Assert.NotNull(responseBody);

        Assert.NotEmpty(responseBody.AvailableTables);

        var table12 = responseBody.AvailableTables.Where(t => t.Available == false).ToList();
        Assert.NotNull(table12);
        Assert.NotEmpty(table12);

        Assert.True(table12.Count == 2);
        Assert.True(table12[0].TableId == 1);
        Assert.True(table12[1].TableId == 2);

    }

    [Fact]
    public async void GetAvailableTables_OverlappingBooking_OneTable_InnerBooking_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,24,11,00,0),
            new DateTime(2022,06,24,12,00,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.AvailableTablesResponse;
        Assert.NotNull(responseBody);

        Assert.NotEmpty(responseBody.AvailableTables);

        var table2 = responseBody.AvailableTables.Where(t => t.Available == false).FirstOrDefault();
        Assert.NotNull(table2);

        Assert.True(table2.TableId == 2);

    }

    [Fact]
    public async void GetAvailableTables_OverlappingBooking_TwoTables_LeftOutBooking_False()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,24,11,30,0),
            new DateTime(2022,06,24,12,30,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.AvailableTablesResponse;
        Assert.NotNull(responseBody);

        Assert.NotEmpty(responseBody.AvailableTables);

        var table12 = responseBody.AvailableTables.Where(t => t.Available == false).ToList();
        Assert.NotNull(table12);
        Assert.NotEmpty(table12);

        Assert.True(table12.Count == 2);
        Assert.True(table12[0].TableId == 1);
        Assert.True(table12[1].TableId == 2);

    }

    [Fact]
    public async void GetAvailableTables_ValidBooking_CountOfTables_True()
    {
        // Arrange
        var tableController = new TableController(_businessLogic);

        // Act
        var response = await tableController.GetAvailableTables(
            1,
            2,
            new DateTime(2022,06,25,10,0,0),
            new DateTime(2022,06,25,11,0,0)
        );

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody = ((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.AvailableTablesResponse;
        Assert.NotNull(responseBody);

        Assert.NotEmpty(responseBody.AvailableTables);

        var tables = responseBody.AvailableTables.Where(t => t.Available == true).ToList();
        Assert.NotNull(tables);
        Assert.NotEmpty(tables);

        Assert.True(tables.Count == 6);
        Assert.True(tables[0].TableId == 1);
        Assert.True(tables[1].TableId == 2);
        Assert.True(tables[2].TableId == 3);
        Assert.True(tables[3].TableId == 4);
        Assert.True(tables[4].TableId == 5);
        Assert.True(tables[5].TableId == 6);

    }
}