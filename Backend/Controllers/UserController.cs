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
using Backend.Helpers;
using Backend.Models;
using Backend.DataAccess;
using Backend.BusinessLogic;

[EnableCors]
[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private DBContext _context;
    private ITokenService _tokenService;

    private readonly IBusinessLogic _businessLogic;

    public UserController(
        DBContext context,
        ITokenService tokenService,
        IBusinessLogic businessLogic
        )
    {
        _context = context;
        _tokenService = tokenService;
        _businessLogic = businessLogic;

    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginRequest request)
    {
        var loginUser = await _businessLogic.LoginUser(request);

        if(!loginUser.Success) {
            return Unauthorized(
                new {
                    loginUser.ErrorCode,
                    loginUser.Error
                }
            );
        }

        return Ok(loginUser);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        // check if there exists a user with provided email
        var userExists = _context.Users.SingleOrDefault(user => user.Email == request.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status400BadRequest, "User already exists!");

        // hash password
        var hpass = BCrypt.HashPassword(request.Password);

		// create new user
		User user = new User()
		{
			Email = request.Email,
			Password = hpass,
			Name = request.Name,
			PhoneNo = request.PhoneNo,
			Role = Role.Customer
		};

		//write user to db
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Ok("User created successfully!");
    }

    [HttpGet("{id}")]
    [Authorize(Roles = nameof(Role.Customer))]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return user;
    }


    [HttpGet]
    [Authorize(Roles = nameof(Role.RestaurantManager) + "," + nameof(Role.Admin))]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}