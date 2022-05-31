using System;
using System.Net;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Backend.BusinessLogic;
using Backend.Controllers;
using Backend.Helpers;
using Backend.DataAccess;
using Backend.Helpers.Models.Requests;

public class LoginTest : IDisposable
{
    private readonly DBContext _dbContext;
    private readonly TokenService _tokenService;
    private readonly UserBL _userBL;
    private readonly BusinessLogic _businessLogic;


    public LoginTest()
    {
        // set db
        // options for db
        var dbOption = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Data Source=LAPTOP-D5VQT9SU;Initial Catalog=BookingSystem;Integrated Security=True;")
            .Options;
        _dbContext = new DBContext(dbOption);

        // set token service
        // configuration for token service
        var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:ValidAudience", "https://localhost:7076"},
                {"JWT:ValidIssuer", "https://localhost:7076"},
                {"JWT:Secret", "JWTsecuredPasswordHGbnj452Khrd"},

            };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
        _tokenService = new TokenService(configuration);

        // set userBL
        _userBL = new UserBL(_dbContext, _tokenService);

        // set business logic
        _businessLogic = new BusinessLogic(_userBL, null, null, null);

    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async void Login_FindUser_ByEmail_True()
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "user@email.com",
            Password = "password"
        });


        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async void Login_FIndUser_ByEmail_False() 
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "user1@email.com",
            Password = "password"
        });


        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Login_ValidPassword_True() 
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "user@email.com",
            Password = "password"
        });


        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async void Login_InvalidPassword_False() 
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "user@email.com",
            Password = "passwords"
        });


        // Assert
        response.Should().BeOfType<UnauthorizedObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Login_SignInCustomer_True() 
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "user@email.com",
            Password = "password"
        });


        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.TokenResponse;
        Assert.NotNull(responseBody);

        var user = responseBody.User;
        Assert.NotNull(user);

        var role = user.Role;
        Assert.NotNull(role);

        Assert.Equal(role.Claims, "Customer");
    }

    [Fact]
    public async void Login_SignInRestaurantManager_True() 
    {
        // Arrange
        var userController = new UserController(_businessLogic);

        // Act
        var response = await userController.LoginUser(new LoginRequest()
        {
            Email = "manager@email.com",
            Password = "password"
        });


        // Assert
        response.Should().BeOfType<OkObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var responseBody =((OkObjectResult)response).Value as Backend.Helpers.Models.Responses.TokenResponse;
        Assert.NotNull(responseBody);

        var user = responseBody.User;
        Assert.NotNull(user);

        var role = user.Role;
        Assert.NotNull(role);

        Assert.Equal(role.Claims, "RestaurantManager");

        var restaurants = responseBody.Restaurants;
        Assert.NotNull(restaurants);
        Assert.NotEmpty(restaurants);

        Assert.Equal(restaurants[0].Id, 1);
    }

}