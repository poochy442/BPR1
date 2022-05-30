namespace Backend.BusinessLogic;

using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using BCrypt.Net;
using Backend.Helpers;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess;
using Backend.DataAccess.Models;

public class UserBL : IUserBL
{
    private readonly DBContext _context;
    private readonly ITokenService _tokenService;

    public UserBL(DBContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<GetUsersResponse> GetUsers()
    {
        var users = await _context.Users.Where(u => u.Role.Claims == UserRoles.Customer).ToListAsync();

        return new GetUsersResponse()
        {
            Success = true,
            Users = users
        };
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

                // get rid of loop of user -> role -> list of users -> user...
                user.Role.Users = null;

                // construct response based on user role
                if (userRole.Role.Claims == UserRoles.Customer)
                {
                    return new TokenResponse()
                    {
                        Success = true,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        User = user
                    };
                }
                else if (userRole.Role.Claims == UserRoles.RestaurantManager)
                {
                    // retrieve restaurant(s) manager manages
                    var restaurants = _context.Restaurants.AsNoTracking().Where(r => r.UserId == user.Id).ToList();

                    return new TokenResponse()
                    {
                        Success = true,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        User = user,
                        Restaurants = restaurants
                    };
                }
                else
                {
                    return null;
                }
            }

            else
            {
                return new TokenResponse()
                {
                    Success = false,
                    Error = "Couldn't retrive user's role",
                    ErrorCode = "500"
                };
            }
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


    public async Task<TokenResponse> AutoLogin(int userId, Claim claims)
    {
        // check if user exists
        var user = await _context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return new TokenResponse()
            {
                Success = false,
                Error = "Couldnt find user",
                ErrorCode = "404"
            };
        }

        // get rid of loop of user -> role -> list of users -> user...
        user.Role.Users = null;

        // check customer claims
        if (claims.Value == UserRoles.Customer)
        {
            return new TokenResponse()
            {
                Success = true,
                User = user
            };
        }


        // check manager claims
        if (claims.Value == UserRoles.RestaurantManager)
        {
            var restaurants = _context.Restaurants.AsNoTracking().Where(r => r.UserId == userId).ToList();

            return new TokenResponse()
            {
                Success = true,
                User = user,
                Restaurants = restaurants
            };
        }

        return null;
    }


    public async Task<RegisterUserResponse> RegisterUser(RegisterRequest request)
    {
        // check if there exists a user with provided email
        var userExists = _context.Users.SingleOrDefault(user => user.Email == request.Email);
        if (userExists != null)
            return new RegisterUserResponse()
            {
                Success = false,
                Error = "User already exists!",
                ErrorCode = "500"
            };

        // hash password
        var hpass = BCrypt.HashPassword(request.Password);

        // get user role
        var role = _context.Roles.SingleOrDefault(role => role.Claims == "Customer");

        // if couldnt get user role 
        if (role == null)
        {
            return new RegisterUserResponse()
            {
                Success = false,
                Error = "Couldn't retrive role from db",
                ErrorCode = "500"
            };
        }


        // create new user
        User user = new User()
        {
            Email = request.Email,
            Password = hpass,
            Name = request.Name,
            PhoneNo = request.PhoneNo,
            Role = role
        };

        //write user to db
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        //login the newly created user
        var login = await LoginUser(new LoginRequest(){
            Email = request.Email,
            Password = request.Password
        });

        return new RegisterUserResponse()
        {
            Success = true,
            SuccessMessage = "User created successfully!",
            TokenResponse = login
        };


    }
}