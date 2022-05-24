namespace Backend.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.DataAccess;

public interface ITokenService
{
    JwtSecurityToken GetToken(HttpContext context, DBContext dbContext, string token);
}