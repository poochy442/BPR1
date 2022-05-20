using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;

using Backend.Models;
using Backend.Services;

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