namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using BCrypt.Net;
using Backend.Helpers;
using Backend.Helpers.Models;
using Backend.DataAccess;

public class UserBL : IUserBL
{
    private readonly DBContext _context;
    private readonly ITokenService _tokenService;

    public UserBL(DBContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> LoginUser(LoginRequest request)
    {
        // find user with provided email
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Email == request.Email);

        // check user is found
        if (user == null)
        {
            return new TokenResponse()
            {
                Success = false,
                Error = "Email not found",
                ErrorCode = "404"
            };
        }

        // check password is correct
        if (BCrypt.Verify(request.Password, user.Password))
        {
			// create JWT token
			var token = _tokenService.GetToken();

			return new TokenResponse()
			{
				Success = true,
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			};
        }
        else
        {
            return new TokenResponse()
            {
                Success = false,
                Error = "Incorrect password",
                ErrorCode = "401"
            };
        }
    }
}