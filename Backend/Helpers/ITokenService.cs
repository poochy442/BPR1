namespace Backend.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public interface ITokenService
{
    JwtSecurityToken GetToken(List<Claim> claims);
}