namespace Backend.Controllers;

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
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.Helpers;
using Backend.DataAccess.Models;
using Backend.DataAccess;
using Backend.BusinessLogic;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private DBContext _context;
    private readonly IBusinessLogic _businessLogic;

    public UserController(
        DBContext context,
        IBusinessLogic businessLogic
        )
    {
        _context = context;
        _businessLogic = businessLogic;

    }

    // manager
    [HttpGet("users")]
    //[Authorize(Roles = UserRoles.RestaurantManager)]
    [AllowAnonymous]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _businessLogic.GetUsers();

        return Ok(users.Users);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginRequest request)
    {
        var loginUser = await _businessLogic.LoginUser(request);

        if (!loginUser.Success)
        {
            return Unauthorized(
                new
                {
                    loginUser.ErrorCode,
                    loginUser.Error
                }
            );
        }

        return Ok(loginUser);
    }

    [AllowAnonymous]
    [HttpPost("login-manager")]
    public async Task<ActionResult> LoginManager(LoginRequest request)
    {
        var loginManager = await _businessLogic.LoginManager(request);

        if (!loginManager.Success)
        {
            return Unauthorized(
                new
                {
                    loginManager.ErrorCode,
                    loginManager.Error
                }
            );
        }

        return Ok(loginManager);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterRequest request)
    {
        var registerUser = await _businessLogic.RegisterUser(request);

        if (!registerUser.Success)
        {
            return Unauthorized(
                new
                {
                    registerUser.ErrorCode,
                    registerUser.Error
                }
            );
        }

        return Ok(registerUser);

    }

    // [HttpGet("{id}")]
    // [Authorize(Roles = UserRoles.Customer)]
    // public async Task<ActionResult<User>> GetUser(int id)
    // {
    //     var user = await _context.Users.FindAsync(id);
    //     if (user == null) return NotFound();

    //     return user;
    // }



}