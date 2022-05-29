using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Backend.DataAccess.Models;
using Backend.Helpers.Models.Requests;
using Backend.BusinessLogic;
using Backend.Helpers.Models.Responses;
using Backend.Helpers.Models;
using System.Security.Claims;

namespace Backend.Controllers;

[EnableCors]
[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBusinessLogic _businessLogic;

    public BookingController(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    [HttpGet("customer-current-bookings")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<ActionResult<List<Booking>>> GetCurrentBookingsForCustomer()
    {

        // get claims out of token
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;
        var userId = claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();

        // checkif claims valid
        if (userId == null)
        {
            return StatusCode(500, "invalid customer claims");
        }

        var customerId = Int32.Parse(userId.Value);
        var bookings = await _businessLogic.GetCurrentBookingsForCustomer(customerId);

        if (!bookings.Success)
        {
            return Unauthorized(new
            {
                bookings.ErrorCode,
                bookings.Error
            });
        }

        return Ok(bookings);

    }

    [HttpGet("customer-past-bookings")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<ActionResult<List<Booking>>> GetPastBookingsForCustomer()
    {

        // get claims out of token
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;
        var userId = claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();

        // checkif claims valid
        if (userId == null)
        {
            return StatusCode(500, "invalid customer claims");
        }

        var customerId = Int32.Parse(userId.Value);
        var bookings = await _businessLogic.GetPastBookingsForCustomer(customerId);

        if (!bookings.Success)
        {
            return Unauthorized(new
            {
                bookings.ErrorCode,
                bookings.Error
            });
        }

        return Ok(bookings);

    }

    // manager
    [HttpGet("bookings-for-tables")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
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
    [Authorize(Roles = UserRoles.RestaurantManager)]
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


    // for manager
    [HttpDelete("delete")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> DeleteBooking(long bookingId)
    {

        var deleteBooking = await _businessLogic.DeleteBooking(bookingId);

        if (!deleteBooking.Success)
        {
            return Unauthorized(
                new
                {
                    deleteBooking.ErrorCode,
                    deleteBooking.Error
                }
            );
        }

        return Ok(deleteBooking.SuccessMessage);

    }

    // for customer
    [HttpDelete("cancel")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<ActionResult> CancelBooking(long bookingId)
    {

        var cancelBooking = await _businessLogic.CancelBooking(bookingId);

        if (!cancelBooking.Success)
        {
            return Unauthorized(
                new
                {
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


}