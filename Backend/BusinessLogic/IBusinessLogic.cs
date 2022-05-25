namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models;

public interface IBusinessLogic
{
    Task<TokenResponse> LoginUser(LoginRequest request);
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
}