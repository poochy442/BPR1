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

public class GetRestaurantsByLocationTest : IDisposable
{
    private readonly ITestOutputHelper output;
    private readonly DBContext _dbContext;
    private readonly IRestaurantService _restaurantService;
    private readonly RestaurantBL _restaurantBL;
    private readonly BusinessLogic _businessLogic;

    public GetRestaurantsByLocationTest(ITestOutputHelper output)
    {
        // set output
        this.output = output;

        // set db
        // options for db
        var dbOption = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;")
            .Options;
        _dbContext = new DBContext(dbOption);

        // set restaurant service
        // configuration for restaurant service
        var myConfiguration = new Dictionary<string, string>
            {
                {"ConnectionStrings:Default", "Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;"},
            };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
        _restaurantService = new RestaurantService(configuration);


        // set restaurantBL
        _restaurantBL = new RestaurantBL(_dbContext, _restaurantService);

        // set business logic
        _businessLogic = new BusinessLogic(null, null, null, _restaurantBL);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async void GetRestauarants_ValidAddress_True()
    {
        // Arrange
        var restaurantController = new RestaurantController(_businessLogic);

        // Act
        var response = await restaurantController.GetRestaurantsByLocation(new RestaurantsByLocationRequest()
        {
            Country = "DK",
            PostalCode = "8700",
            City = "Horsens",
            Street = "Kamtjatka",
            StreetNo = "10",
            Radius = 1000
        });

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async void GetRestauarants_InvalidAddress_False()
    {
        // Arrange
        var restaurantController = new RestaurantController(_businessLogic);

        // Act
        var response = await restaurantController.GetRestaurantsByLocation(new RestaurantsByLocationRequest()
        {
            Country = "aa",
            PostalCode = "0000",
            City = "zz",
            Street = "plumbum",
            StreetNo = "00",
            Radius = 1000
        });

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void GetRestauarants_RestaurantsFound_True()
    {
        // Arrange
        var restaurantController = new RestaurantController(_businessLogic);

        // Act
        var response = await restaurantController.GetRestaurantsByLocation(new RestaurantsByLocationRequest()
        {
            Country = "DK",
            PostalCode = "8700",
            City = "Horsens",
            Street = "Kamtjatka",
            StreetNo = "10",
            Radius = 1000
        });

        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody = ((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.GetRestaurantsResponse;
        Assert.NotNull(responseBody);

        var restaurants = responseBody.Restaurants;
        Assert.NotEmpty(restaurants);

        Assert.True(restaurants.Count == 4); 
    }

    [Fact]
    public async void GetRestauarants_RestaurantsFound_False()
    {
        // Arrange
        var restaurantController = new RestaurantController(_businessLogic);

        // Act
        var response = await restaurantController.GetRestaurantsByLocation(new RestaurantsByLocationRequest()
        {
            Country = "DK",
            PostalCode = "8700",
            City = "Horsens",
            Street = "Kamtjatka",
            StreetNo = "10",
            Radius = 50
        });

        // Assert
        response.Should().BeOfType<BadRequestObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

}