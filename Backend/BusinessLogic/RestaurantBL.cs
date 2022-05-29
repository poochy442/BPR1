namespace Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Text.Json;
using Backend.Helpers;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess;

public class RestaurantBL : IRestaurantBL
{
    private readonly DBContext _context;

    public RestaurantBL(DBContext context)
    {
        _context = context;
    }

    public async Task<GetRestaurantsResponse> GetRestaurants(string city) {

        var restaurants =  await _context.Restaurants.Where(r => r.Address.City == city).ToListAsync();

        return new GetRestaurantsResponse() {
            Success = true,
            Restaurants = restaurants
        };
    }

}