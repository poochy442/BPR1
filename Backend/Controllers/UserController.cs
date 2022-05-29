namespace Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.DataAccess.Models;
using Backend.BusinessLogic;

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


    // // manager
    // [HttpPost("login-manager")]
    // [Authorize(Roles = UserRoles.RestaurantManager)]
    // public async Task<ActionResult> LoginManager(LoginRequest request)
    // {
    //     var loginManager = await _businessLogic.LoginManager(request);

    //     if (!loginManager.Success)
    //     {
    //         return Unauthorized(
    //             new
    //             {
    //                 loginManager.ErrorCode,
    //                 loginManager.Error
    //             }
    //         );
    //     }

    //     return Ok(loginManager);
    // }

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