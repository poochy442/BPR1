using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using System.Text.Json.Serialization;

using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[EnableCors]
[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
	private readonly DBContext _context;

	public RestaurantController(DBContext context)
	{
		_context = context;

		if(context.Restaurants.Count() == 0)
		{
			List<string> ft1 = new List<string>(new string[] {"Kebab", "Pizza", "Durum"});
			string ft1String = JsonSerializer.Serialize(ft1);
			List<long> t1 = new List<long>(new long[] {1, 2, 3, 4});
			string t1String = JsonSerializer.Serialize(t1);
			PostRestaurant(new Restaurant(1, t1String, "Sundvej 8", 12.8f, 28f, "Delon\'s", ft1String));
			List<string> ft2 = new List<string>(new string[] {"Fastfood", "Dessert"});
			string ft2String = JsonSerializer.Serialize(ft2);
			List<long> t2 = new List<long>(new long[] {5, 6, 7, 8, 9, 10});
			string t2String = JsonSerializer.Serialize(t2);
			PostRestaurant(new Restaurant(2, t2String, "Sundvej 10", 12.9f, 28f, "McDonald\'s", ft2String));
			List<string> ft3 = new List<string>(new string[] {"Ribs", "Burger"});
			string ft3String = JsonSerializer.Serialize(ft3);
			List<long> t3 = new List<long>(new long[] {11, 12, 13});
			string t3String = JsonSerializer.Serialize(t3);
			PostRestaurant(new Restaurant(3, t3String, "Sundvej 12", 13f, 28f, "Bone\'s", ft3String));
		}
	}

	[Route("")]
	[HttpGet]
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

}