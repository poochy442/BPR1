namespace Backend.BusinessLogic;

using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
public interface IRestaurantBL
{
    Task<GetRestaurantsResponse> GetRestaurants(string city);
    Task<GetRestaurantsResponse> GetRestaurantsByLocation(RestaurantsByLocationRequest request);
}