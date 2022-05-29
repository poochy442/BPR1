using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Backend.BusinessLogic;
using Backend.DataAccess.Models;
using Backend.DataAccess;
using Backend.Helpers;
using Backend.Helpers.Models.Requests;

namespace Backend.Controllers;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly DBContext _context;
    private readonly IBusinessLogic _businessLogic;

	private readonly RestaurantService restaurantService;

    public RestaurantController(DBContext context, IBusinessLogic businessLogic)
    {
        _context = context;
        _businessLogic = businessLogic;
		restaurantService = new RestaurantService();
    }

    // [AllowAnonymous]
    // [HttpGet]
    // public async Task<ActionResult<Restaurant>> GetRestaurnats()
    // {
    //     return await _context.Restaurants.FindAsync(1);
    // }

    // customer
    [AllowAnonymous]
    [HttpGet("restaurants")]
    public async Task<ActionResult<List<Restaurant>>> GetRestaurants(string city)
    {
        var restaurants = await _businessLogic.GetRestaurants(city);

        return Ok(restaurants.Restaurants);
    }

    [HttpGet("restaurants-location")]
    [AllowAnonymous]
    public async Task<ActionResult> GetRestaurantsByLocation(RestaurantsByLocationRequest request)
    {

		var location = await restaurantService.GetAddressLocation(request);
		var restaurants = restaurantService.ExecuteDbQuery(55.861690m, 9.858280m, request.Radius);
		return Ok(new {
			location = location,
			restaurants = restaurants
		});
    }

    /*[HttpGet]
    public Task<List<Restaurant>> GetRestaurants()
    {
        return _context.Restaurants.ToListAsync();
    }

	[HttpGet("{id}")]
	public async Task<ActionResult<Restaurant>> GetRestaurant(long id)
	{
		var restaurant = await _context.Restaurants.FindAsync(id);

		if(restaurant == null)
		{
			return NotFound();
		}

		return restaurant;
	}

	[HttpPost]
	public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
	{
		_context.Restaurants.Add(restaurant);
		await _context.SaveChangesAsync();

		return CreatedAtAction(
			nameof(GetRestaurant),
			new { id = restaurant.Id },
			restaurant
		);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutRestaurant(long id, Restaurant restaurant)
	{
		if(id != restaurant.Id)
		{
			return BadRequest();
		}

		var putRestaurant = await _context.Restaurants.FindAsync(restaurant.Id);
		if(putRestaurant == null)
		{
			return NotFound();
		}

		putRestaurant = restaurant;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException) when (!RestaurantExists(id))
		{
			return NotFound();
		}

		return NoContent();
	}

	private bool RestaurantExists(long id)
	{
		return _context.Restaurants.Any(e => e.Id == id);
	}
*/
}