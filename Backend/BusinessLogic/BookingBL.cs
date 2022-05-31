namespace Backend.BusinessLogic;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess;
using Backend.DataAccess.Models;

public class BookingBL : IBookingBL
{
    private readonly DBContext _context;

    public BookingBL(DBContext context)
    {
        _context = context;
    }

    public async Task<GetBookingsResponse> GetCurrentBookingsForCustomer(int customerId)
    {
        // check if customer exists
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == customerId);
        if (user == null)
        {
            return new GetBookingsResponse()
            {
                Success = false,
                Error = "Couldnt find user",
                ErrorCode = "404"
            };
        }

        // retrieve customer bookings
        var bookings = await _context.Bookings
        .AsNoTracking()
        .Include(b => b.Table)
        .Include(b => b.Restaurant)
        .ThenInclude(r => r.Address)
        .Where(b => b.UserId == customerId && b.StartDate > DateTime.Now)
        .OrderBy(b => b.StartDate)
        .ToListAsync();

        return new GetBookingsResponse()
        {
            Success = true,
            Bookings = bookings
        };
    }

    public async Task<GetBookingsResponse> GetPastBookingsForCustomer(int customerId)
    {
        // check if customer exists
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == customerId);
        if (user == null)
        {
            return new GetBookingsResponse()
            {
                Success = false,
                Error = "Couldnt find user",
                ErrorCode = "404"
            };
        }

        // retrieve customer bookings
        var bookings = await _context.Bookings
        .AsNoTracking()
        .Include(b => b.Table)
        .Include(b => b.Restaurant)
        .ThenInclude(r => r.Address)
        .Where(b => b.UserId == customerId && b.StartDate <= DateTime.Now)
        .OrderByDescending(b => b.StartDate)
        .ToListAsync();

        return new GetBookingsResponse()
        {
            Success = true,
            Bookings = bookings
        };
    }

    public async Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId)
    {
        // check if there exists restaurant
        var restaurantExists = await _context.Restaurants
        .AsNoTracking()
        .FirstOrDefaultAsync(r => r.Id == restaurantId);

        if (restaurantExists == null)
        {
            return new GetTableBookingsResponse()
            {
                Success = false,
                Error = "404",
                ErrorCode = "Couldnt find restaurant with id:" + restaurantId
            };
        }

        // join request to get bookings of tables
        var joinBookings = await _context.Tables
        .AsNoTracking()
        .Join(_context.Bookings.Include(b => b.User),
        t => t.Id,
        b => b.TableId,
        (t, b) => new
        {
            Table = t,
            Booking = b
        }
        )
        .Where(join => join.Table.RestaurantId == restaurantId
                    && join.Booking.RestaurantId == restaurantId)
        .ToListAsync();

        // check if there are bookings of tables
        if (joinBookings == null || joinBookings.Count == 0)
        {
            return new GetTableBookingsResponse()
            {
                Success = false,
                Error = "500",
                ErrorCode = "Couldn't retrieve bookings of tables"
            };
        }


        // building response
        #region BuildResponse

        // response variable
        var response = new List<TableBooking>();

        // saving tableNo for the loop
        var tableNo = joinBookings[0].Table.TableNo;

        // create object for TableBooking
        var tableBooking = new TableBooking()
        {
            TableNo = tableNo,
            Bookings = new List<Booking>()
        };

        // iterate through joinBookings 
        for (int i = 0; i < joinBookings.Count; i++)
        {

            if (joinBookings[i].Table.TableNo == tableNo)
            {
                tableBooking.Bookings.Add(joinBookings[i].Booking);
            }

            else
            {
                // set new tableNo
                tableNo = joinBookings[i].Table.TableNo;

                // add TableBooking to response variale
                response.Add(tableBooking);

                // create new TableBooking
                tableBooking = new TableBooking()
                {
                    TableNo = tableNo,
                    Bookings = new List<Booking>()
                };

                // add current Booking to new TableBooking
                tableBooking.Bookings.Add(joinBookings[i].Booking);
            }

            // add the last TableBooking
            if (i == joinBookings.Count - 1)
            {
                response.Add(tableBooking);
            }
        }

        #endregion

        return new GetTableBookingsResponse()
        {
            Success = true,
            TableBookings = response
        };
    }

    public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request)
    {
        // check if there is already such a booking
        var bookingExists = await _context.Bookings
        .AsNoTracking()
        .Where(b => b.Date == request.Date
        && b.StartDate == request.StartDate
        && b.EndDate == request.EndDate
        && b.RestaurantId == request.RestaurantId
        && b.TableId == request.TableId
        && b.UserId == request.UserId)
        .FirstOrDefaultAsync();

        if (bookingExists != null)
        {
            return new CreateBookingResponse()
            {
                Success = false,
                Error = "There is already such a booking",
                ErrorCode = "500"
            };
        }


        // check if there exists a user with provided id
        var userExists = await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(user => user.Id == request.UserId);

        if (userExists == null)
        {
            return new CreateBookingResponse()
            {
                Success = false,
                Error = "There isn't a user with provided Id",
                ErrorCode = "500"
            };
        }


        // check if table is valid
        var tableValid = await _context.Tables
        .AsNoTracking()
        .Where(table => table.Id == request.TableId && table.RestaurantId == request.RestaurantId)
        .FirstOrDefaultAsync();

        if (tableValid == null)
        {
            return new CreateBookingResponse()
            {
                Success = false,
                Error = "Either table Id is invalid or table doesn't belong to the restaurant(restaurantId)",
                ErrorCode = "500"
            };
        }


        // create new booking
        Booking b = new Booking()
        {
            Date = request.Date,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            GuestNo = request.GuestNo,
            Note = request.Note,
            TableId = tableValid.Id,
            UserId = userExists.Id,
            RestaurantId = tableValid.RestaurantId
        };

        // write booking to db
        _context.Bookings.Add(b);
        await _context.SaveChangesAsync();

        return new CreateBookingResponse()
        {
            Success = true,
            SuccessMessage = "Booking created successfully!"
        };
    }

    public async Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request)
    {
        // check if there is already such a booking
        var bookingExists = await _context.Bookings
        .AsNoTracking()
        .Where(b => b.Date == request.Date
        && b.StartDate == request.StartDate
        && b.EndDate == request.EndDate
        && b.RestaurantId == request.RestaurantId
        && b.TableId == request.TableId)
        .FirstOrDefaultAsync();

        if (bookingExists != null)
        {
            return new CreateBookingResponse()
            {
                Success = false,
                Error = "There is already such a booking",
                ErrorCode = "500"
            };
        }

        // check if table is valid
        var tableValid = await _context.Tables
        .AsNoTracking()
        .Where(table => table.Id == request.TableId && table.RestaurantId == request.RestaurantId)
        .FirstOrDefaultAsync();

        if (tableValid == null)
        {
            return new CreateBookingResponse()
            {
                Success = false,
                Error = "Either table Id is invalid or table doesn't belong to the restaurant(restaurantId)",
                ErrorCode = "500"
            };
        }


        // create new booking
        Booking b = new Booking()
        {
            Date = request.Date,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            GuestNo = request.GuestNo,
            Note = request.Note,
            TableId = tableValid.Id,
            RestaurantId = tableValid.RestaurantId
        };

        // write booking to db
        _context.Bookings.Add(b);
        await _context.SaveChangesAsync();

        return new CreateBookingResponse()
        {
            Success = true,
            SuccessMessage = "Booking created successfully!"
        };
    }

    public async Task<DeleteBookingResponse> DeleteBooking(long id)
    {

        // check if booking exists
        var booking = await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return new DeleteBookingResponse()
            {
                Success = false,
                Error = "Couldn't find booking with id: " + id,
                ErrorCode = "404"
            };
        }

        // remove booking
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return new DeleteBookingResponse()
        {
            Success = true,
            SuccessMessage = "Booking deleted"
        };
    }

    public async Task<DeleteBookingResponse> CancelBooking(long id)
    {

        // check if booking exists
        var booking = await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return new DeleteBookingResponse()
            {
                Success = false,
                Error = "Couldn't find booking with id: " + id,
                ErrorCode = "404"
            };
        }

        // check if booking can be deleted given Table's deadline for cancellation
        var table = await _context.Tables.Where(t => t.Id == booking.TableId).FirstOrDefaultAsync();
        var deadline = table.Deadline.Value;
        var now = DateTime.Now;
        var nowWithDeadline = new DateTime(now.Year, now.Month, now.Day,
                                           now.Hour + deadline.Hour, now.Minute + deadline.Minute, now.Second);
        var difference = nowWithDeadline - booking.StartDate;
        if (difference.Value.TotalMilliseconds > 0)
        {
            return new DeleteBookingResponse()
            {
                Success = false,
                Error = "Too late to cancel booking, deadline is: " + deadline.Hour + " hour(s), " + deadline.Minute + " minutes",
                ErrorCode = "404"
            };
        }

        // remove booking
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return new DeleteBookingResponse()
        {
            Success = true,
            SuccessMessage = "Booking deleted"
        };


    }
}