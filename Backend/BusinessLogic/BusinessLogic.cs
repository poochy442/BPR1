namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models;
public class BusinessLogic : IBusinessLogic
{
    private readonly IUserBL _userBL;

    public BusinessLogic(
      IUserBL userBL
      )
    {
        _userBL = userBL;
    }

    public async Task<TokenResponse> LoginUser(LoginRequest request) {
        return await _userBL.LoginUser(request);
    }

}



