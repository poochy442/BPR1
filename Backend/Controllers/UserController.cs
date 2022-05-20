namespace Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Helpers;
using Backend.Models;
using Backend.Services;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private DBContext _context;
	private readonly AppSettings _appSettings;

    public UserController(DBContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
		_appSettings = appSettings.Value;
    }
/*
	private string GenerateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpPost("Authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticateRequest request)
    {
		var user = _context.Users.SingleOrDefault(user => user.Email == request.Email && user.Password == request.Password);
        if (user == null) return NotFound();

        var token = GenerateJwtToken(user);
        return new AuthenticationResponse(user, token);
    }

	[HttpPost("Register")]
	public async Task<ActionResult<AuthenticationResponse>> Register(RegisterRequest request)
	{
		User user = new User(request.Email, request.Password, request.Name, request.PhoneNo);
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		var token = GenerateJwtToken(user);
		return new AuthenticationResponse(user, token);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(long id)
	{
		var user = await _context.Users.FindAsync(id);
		if(user == null) return NotFound();

		return user;
	}

	[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return _context.Users.ToList();
    }
    */
}