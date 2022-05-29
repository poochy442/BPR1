namespace Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.DataAccess.Models;
using Backend.BusinessLogic;
using System.Security.Claims;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IBusinessLogic _businessLogic;

    public UserController(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    // manager
    [HttpGet("users")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _businessLogic.GetUsers();

        return Ok(users.Users);
    }

    // anonymous
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


    // anonymous
    [AllowAnonymous]
    [HttpPost("autologin")]
    public async Task<ActionResult> AutoLogin()
    {
        // get claims out of token
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;
        var userId = claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
        var userRole = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();

        // checkif claims valid
        if (userId == null || userRole == null)
        {
            return StatusCode(500, "invalid customer claims(id/role)");
        }

        var autologin = await _businessLogic.AutoLogin(Int32.Parse(userId.Value), userRole);

        if (!autologin.Success)
        {
            return Unauthorized(new
            {
                autologin.ErrorCode,
                autologin.Error
            });
        }

        return Ok(autologin);
    }

    // anonymous
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