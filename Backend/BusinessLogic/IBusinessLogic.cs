namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models;

public interface IBusinessLogic
{
    Task<TokenResponse> LoginUser(LoginRequest request);
}