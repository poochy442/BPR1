namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface IUserBL
{
    Task<GetUsersResponse> GetUsers();
    Task<TokenResponse> LoginUser(LoginRequest request);

    Task<RegisterUserResponse> RegisterUser(RegisterRequest request);
}