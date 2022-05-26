namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface IUserBL
{
    Task<TokenResponse> LoginUser(LoginRequest request);
}