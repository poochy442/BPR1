namespace Backend.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Backend.Models;

public interface ITokenService
{
    JwtSecurityToken GetToken(Role role);
}