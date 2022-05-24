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
using Backend.Helpers;
using Backend.DataAccess.Models;
using Backend.DataAccess;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private DBContext _context;
    private ITokenService _tokenService;

    public UserController(
        DBContext context,
        ITokenService tokenService
        )
    {
        _context = context;
        _tokenService = tokenService;

    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public ActionResult Authenticate(LoginModel model)
    {
        // find user with provided email
        var user = _context.Users.SingleOrDefault(user => user.Email == model.Email);

        // check user is found
        // check password is correct
        if (user != null && BCrypt.Verify(model.Password, user.Password))
        {
            // extract user role
            var userRole = _context.Users.Include(u => u.Role).Where(u => u.Email == user.Email).FirstOrDefault();

            // if successfully extracted user role
            if (userRole != null)
            {
                // prepare user claims for JWT token
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, userRole.Role.Claims),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // create JWT token
                var token = _tokenService.GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't retrive user's role");
            }
        }
        return Unauthorized("Username or password is incorrect");
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterModel model)
    {
        // check if there exists a user with provided email
        var userExists = _context.Users.SingleOrDefault(user => user.Email == model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

        // hash password
        var hpass = BCrypt.HashPassword(model.Password);

        // get user role
        var role = _context.Roles.SingleOrDefault(role => role.Claims == "Customer");

        // if successfully got user role 
        if (role != null)
        {
            // create new user
            User user = new User()
            {
                Email = model.Email,
                Password = hpass,
                Name = model.Name,
                PhoneNo = model.PhoneNo,
                Role = role
            };

            //write user to db
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User created successfully!");
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't retrive role from db");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return user;
    }


    [HttpGet]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}