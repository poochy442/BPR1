using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Backend.DataAccess.Models;
using Backend.DataAccess;
using Backend.Helpers.Models.Requests;
using Backend.BusinessLogic;
using Backend.DataAccess.Models;
using Backend.Helpers.Models.Responses;
using Backend.Helpers.Models;

namespace Backend.Controllers;

[EnableCors]
[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly DBContext _context;
    private readonly IBusinessLogic _businessLogic;

    public BookingController(DBContext context, IBusinessLogic businessLogic)
    {
        _context = context;
        _businessLogic = businessLogic;
    }

    // [HttpGet]
    // public Task<List<Booking>> GetBookings()
    // {
    //     return _context.Bookings.ToListAsync();
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Booking>> GetBooking(long id)
    // {
    //     var booking = await _context.Bookings.FindAsync(id);

    //     if (booking == null)
    //     {
    //         return NotFound();
    //     }

    //     return booking;
    // }

    // manager
    [HttpGet("bookings-for-tables")]
    [AllowAnonymous]
    public async Task<ActionResult<GetTableBookingsResponse>> GetBookingsForTables(long restaurantId)
    {

        var getTableBookings = await _businessLogic.GetBookingsForTables(restaurantId);

        if (!getTableBookings.Success)
        {
            return Unauthorized(
                new
                {
                    getTableBookings.ErrorCode,
                    getTableBookings.Error
                }
            );
        }

        return Ok(getTableBookings);
    }

    // both customer and manager
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateBooking(CreateBookingRequest request)
    {

        var createBooking = await _businessLogic.CreateBooking(request);

        if (!createBooking.Success)
        {
            return Unauthorized(
                new
                {
                    createBooking.ErrorCode,
                    createBooking.Error
                }
            );
        }

        return Ok(createBooking);
    }

    // manager
    [HttpPost("incall-booking")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateInCallBooking(CreateInCallBookingRequest request)
    {

        var createBooking = await _businessLogic.CreateInCallBooking(request);

        if (!createBooking.Success)
        {
            return Unauthorized(
                new
                {
                    createBooking.ErrorCode,
                    createBooking.Error
                }
            );
        }

        return Ok(createBooking);
    }



    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutBooking(long id, Booking booking)
    // {
    //     if (id != booking.Id)
    //     {
    //         return BadRequest();
    //     }

    //     var putBooking = await _context.Bookings.FindAsync(booking.Id);
    //     if (putBooking == null)
    //     {
    //         return NotFound();
    //     }

    //     putBooking = booking;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException) when (!BookingExists(id))
    //     {
    //         return NotFound();
    //     }

    //     return NoContent();
    // }

    // for manager
    [HttpDelete("delete")]
    [AllowAnonymous]
    public async Task<ActionResult> DeleteBooking(long bookingId) {

        var deleteBooking = await _businessLogic.DeleteBooking(bookingId);

        if(!deleteBooking.Success) {
            return Unauthorized(
                new {
                    deleteBooking.ErrorCode,
                    deleteBooking.Error
                }
            );
        }

        return Ok(deleteBooking.SuccessMessage);

    }

    [HttpDelete("cancel")]
    [AllowAnonymous]
    public async Task<ActionResult> CancelBooking(long bookingId) {

        var cancelBooking = await _businessLogic.CancelBooking(bookingId);

        if(!cancelBooking.Success) {
            return Unauthorized(
                new {
                    cancelBooking.ErrorCode,
                    cancelBooking.Error
                }
            );
        }

        return Ok(cancelBooking.SuccessMessage);

    }

    // private bool BookingExists(long id)
    // {
    //     return _context.Bookings.Any(e => e.Id == id);
    // }

    

}