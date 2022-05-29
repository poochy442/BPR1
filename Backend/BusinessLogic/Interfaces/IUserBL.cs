namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using System.Security.Claims;

public interface IUserBL
{
    Task<GetUsersResponse> GetUsers();
    Task<TokenResponse> LoginUser(LoginRequest request);
    Task<TokenResponse> AutoLogin(int userId, Claim claims);
    Task<RegisterUserResponse> RegisterUser(RegisterRequest request);
}