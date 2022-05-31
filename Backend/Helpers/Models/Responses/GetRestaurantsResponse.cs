using Backend.Helpers.Models.Responses;
using Backend.DataAccess.Models;
namespace Backend.Helpers.Models.Responses;
public class GetRestaurantsResponse : BaseResponse
{
    public List<Restaurant>? Restaurants { get; set; }
}