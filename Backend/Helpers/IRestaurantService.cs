namespace Backend.Helpers;
using Backend.DataAccess.Models;
using Backend.Helpers.Models.Requests;
public interface IRestaurantService
{
    List<Restaurant> ExecuteDbQuery(decimal origLat, decimal origLong, int radius);
    Task<Dictionary<string, decimal>> GetAddressLocation(RestaurantsByLocationRequest req);
}