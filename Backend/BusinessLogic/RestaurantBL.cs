namespace Backend.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Backend.Helpers;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess;

public class RestaurantBL : IRestaurantBL
{
    private readonly DBContext _context;
    private readonly IRestaurantService _restaurantService;

    public RestaurantBL(DBContext context, IRestaurantService restaurantService)
    {
        _context = context;
        _restaurantService = restaurantService;
    }

    

    public async Task<GetRestaurantsResponse> GetRestaurants(string city)
    {

        var restaurants = await _context.Restaurants.Where(r => r.Address.City == city).ToListAsync();

        return new GetRestaurantsResponse()
        {
            Success = true,
            Restaurants = restaurants
        };
    }

    public async Task<GetRestaurantsResponse> GetRestaurantsByLocation(RestaurantsByLocationRequest request)
    {

        // get location coordinates for the address
        var location = await _restaurantService.GetAddressLocation(request);

        if (location.Count == 0)
        {
            return new GetRestaurantsResponse() {
                Success = false,
                ErrorCode = "Couldnt get location for address",
                Error = "500"
            };
        }

        // get restaurants in the area
        var restaurants = _restaurantService.ExecuteDbQuery(location["latitude"], location["longitude"], request.Radius);

        return new GetRestaurantsResponse()
        {
            Success = true,
            Restaurants = restaurants
        };
    }

}