namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models;

public interface IUserBL
{
    Task<TokenResponse> LoginUser(LoginRequest request);
}