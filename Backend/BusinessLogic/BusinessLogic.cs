namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models;
public class BusinessLogic : IBusinessLogic
{
    private readonly IUserBL _userBL;
    private readonly ITableBL _tableBL;

    public BusinessLogic(
      IUserBL userBL,
      ITableBL tableBL
      )
    {
        _userBL = userBL;
        _tableBL = tableBL;
    }

    public async Task<TokenResponse> LoginUser(LoginRequest request) {
        return await _userBL.LoginUser(request);
    }

    public async Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end) {
        return await _tableBL.GetAvailableTables(restaurantId, guests, start, end);
    }

}



