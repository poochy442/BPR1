namespace Backend.Helpers.Models.Responses;
using Backend.DataAccess.Models;

public class GetUsersResponse : BaseResponse
{
    public List<User>? Users {get; set;}
}