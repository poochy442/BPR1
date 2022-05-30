using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Backend.BusinessLogic;
using Backend.DataAccess.Models;
using Backend.Helpers.Models.Requests;

namespace Backend.Controllers;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
	private readonly IBusinessLogic _businessLogic;

    public RestaurantController(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    // customer and manager
    // [AllowAnonymous]
    [HttpGet("restaurants")]
    public async Task<ActionResult<List<Restaurant>>> GetRestaurants([FromQuery] string city)
    {
        var restaurants = await _businessLogic.GetRestaurants(city);

        return Ok(restaurants.Restaurants);
    }

	// customer and manager
    [HttpPost("restaurants-location")]
    // [AllowAnonymous]
    public async Task<ActionResult> GetRestaurantsByLocation([FromBody] RestaurantsByLocationRequest request)
    {

        var restaurants = await _businessLogic.GetRestaurantsByLocation(request);

        if (!restaurants.Success)
        {
            return Unauthorized(new
            {
                restaurants.ErrorCode,
                restaurants.Error
            });
        }

        return Ok(restaurants);

    }

    /*[HttpGet]
    public Task<List<Restaurant>> GetRestaurants()
    {
        return _context.Restaurants.ToListAsync();
    }

	[Route("{id}")]
	[HttpGet]
	public async Task<ActionResult<Restaurant>> GetRestaurant(long id)
	{
		var restaurant = await _context.Restaurants.FindAsync(id);

		if(restaurant == null)
		{
			return NotFound();
		}

		return restaurant;
	}

	[Route("")]
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

	[Route("{id}")]
	[HttpPut]
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

	[Route("Search")]
	[HttpPost]
	public Task<List<Restaurant>> SearchRestaurant()
	{
		return _context.Restaurants.ToListAsync();
	}

	private bool RestaurantExists(long id)
	{
		return _context.Restaurants.Any(e => e.Id == id);
	}
*/
}