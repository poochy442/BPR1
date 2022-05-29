using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;

using Backend.DataAccess.Models;
using Backend.DataAccess;

namespace Backend.Controllers;

[EnableCors]
[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
	private readonly DBContext _context;

	public RatingController(DBContext context)
	{
		_context = context;

		if(context.Ratings.Count() == 0)
		{
			PostRating(new Rating(1, 4, "Pretty good service, decent food"));
			PostRating(new Rating(2, 4, "Pretty good service, decent food"));
			PostRating(new Rating(3, 4, "Pretty good service, decent food"));
			PostRating(new Rating(1, 2, "Pretty bad service"));
			PostRating(new Rating(3, 2, "Pretty bad service"));
			PostRating(new Rating(1, 1, "Absolutely awful"));
			PostRating(new Rating(2, 1, "Absolutely awful"));
		}
	}

	/*[HttpGet]
    public async Task<ActionResult<List<Rating>>> GetRatings(long restaurantId)
    {
		var r = await _context.Restaurants.FindAsync(restaurantId);
		if(r == null) return NotFound();

        List<long> ids = JsonSerializer.Deserialize<List<long>>(r.TableIds) ?? new List<long>();
		List<Rating> ratings = _context.Ratings.ToList().FindAll((element) => ids.Contains(element.Id));
        return ratings;
    }

	[HttpGet("{id}")]
	public async Task<ActionResult<Rating>> GetRating(long id)
	{
		var rating = await _context.Ratings.FindAsync(id);

		if(rating == null)
		{
			return NotFound();
		}

		return rating;
	}

	[HttpPost]
	public async Task<ActionResult<Rating>> PostRating(Rating rating)
	{
		var restaurant = _context.Restaurants.Single(res => res.Id == rating.RestaurantId);
		var ratings = _context.Ratings.Select(rating => rating.RestaurantId == restaurant.Id).ToList();
		restaurant.TotalScore = (restaurant.TotalScore * ratings.Count + rating.Score) / (ratings.Count + 1);

		_context.Ratings.Add(rating);
		await _context.SaveChangesAsync();

		return CreatedAtAction(
			nameof(GetRating),
			new { id = rating.Id },
			rating
		);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutRating(long id, Rating rating)
	{
		if(id != rating.Id)
		{
			return BadRequest();
		}

		var putRating = await _context.Ratings.FindAsync(rating.Id);
		if(putRating == null)
		{
			return NotFound();
		}

		putRating = rating;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException) when (!RatingExists(id))
		{
			return NotFound();
		}

		return NoContent();
	}

	private bool RatingExists(long id)
	{
		return _context.Ratings.Any(e => e.Id == id);
	}*/

}