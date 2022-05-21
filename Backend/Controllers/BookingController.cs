using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using Backend.DataAccess.Models;
using Backend.DataAccess;

namespace Backend.Controllers;

[EnableCors]
[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
	private readonly DBContext _context;

	public BookingController(DBContext context)
	{
		_context = context;
	}

	[HttpGet]
    public Task<List<Booking>> GetBookings()
    {
        return _context.Bookings.ToListAsync();
    }

	/*[HttpGet("{id}")]
	public async Task<ActionResult<Booking>> GetBooking(long id)
	{
		var booking = await _context.Bookings.FindAsync(id);

		if(booking == null)
		{
			return NotFound();
		}

		return booking;
	}

	[HttpPost]
	public async Task<ActionResult<Booking>> PostBooking(Booking booking)
	{
		_context.Bookings.Add(booking);
		await _context.SaveChangesAsync();

		return CreatedAtAction(
			nameof(GetBooking),
			new { id = booking.Id },
			booking
		);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutBooking(long id, Booking booking)
	{
		if(id != booking.Id)
		{
			return BadRequest();
		}

		var putBooking = await _context.Bookings.FindAsync(booking.Id);
		if(putBooking == null)
		{
			return NotFound();
		}

		putBooking = booking;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException) when (!BookingExists(id))
		{
			return NotFound();
		}

		return NoContent();
	}

	private bool BookingExists(long id)
	{
		return _context.Bookings.Any(e => e.Id == id);
	}
*/
}