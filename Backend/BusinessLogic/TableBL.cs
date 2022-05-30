namespace Backend.BusinessLogic;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using Backend.Helpers;
using Backend.Helpers.Models;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess;

public class TableBL : ITableBL
{
    private readonly DBContext _context;

    public TableBL(DBContext context)
    {
        _context = context;
    }

    public async Task<GetTablesResponse> GetTables(long restaurantId)
    {
        // check if there exists restaurant
        var restaurantExists = await _context.Restaurants
        .AsNoTracking()
        .FirstOrDefaultAsync(r => r.Id == restaurantId);

        if (restaurantExists == null)
        {
            return new GetTablesResponse()
            {
                Success = false,
                Error = "Couldnt find restaurant with id:" + restaurantId,
                ErrorCode = "404"
            };
        }

        var tables = await _context.Tables
        .AsNoTracking()
        .Include(t => t.Restriction)
        .Where(t => t.RestaurantId == restaurantId)
        .ToListAsync();

        return new GetTablesResponse()
        {
            Success = true,
            Tables = tables
        };
    }


    public async Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end)
    {
        // find restaurant
        var restaurant = await _context.Restaurants.FindAsync((int)restaurantId);

        // check if could find restaurant
        if (restaurant == null)
        {
            return new AvailableTablesResponse()
            {
                Success = false,
                Error = "Couldnt find restaurant with id:" + restaurantId,
                ErrorCode = "404"
            };
        }

        // extract restaurant's working hours
        var restaurantHours = JsonSerializer.Deserialize<List<WorkingHours>>(restaurant.WorkingHours);

        // check if restaurant's hours are valid
        if (restaurantHours == null || restaurantHours.Count == 0)
        {
            return new AvailableTablesResponse()
            {
                Success = false,
                Error = "restaurant has empty working hours",
                ErrorCode = "500"
            };
        }


        #region ConstructDateTime 
        // base date
        var date = start.Date;

        // find restaurant's open and close time
        var workTime = restaurantHours.Find(wh => wh.Day == ((int)start.DayOfWeek + 5) % 6);

        //  find restaurant's open and close hours
        var openHours = Int32.Parse(workTime.From.Substring(0, 2));
        var closeHours = Int32.Parse(workTime.Till.Substring(0, 2));

        //  find restaurant's open and close minutes
        var openMinutes = Int32.Parse(workTime.From.Substring(3, 2));
        var closeMinutes = Int32.Parse(workTime.Till.Substring(3, 2));

        // build restaurant's open and close datetime objects
        var open = date.Date.Add(new TimeSpan(openHours, openMinutes, 0));
        var close = date.Date.Add(new TimeSpan(closeHours, closeMinutes, 0));
        #endregion


        // check booking time period sorts with restaurant's working hours
        if (start < open || end > close)
        {
            return new AvailableTablesResponse()
            {
                Success = false,
                Error = "time period doesnt fit in working hours of restaurant " + open + "  " + close,
                ErrorCode = "400"
            };
        }

        // retrieve tables from db
        var tables = _context.Tables.ToList();

        // check retrieved some tables
        if (tables == null || tables.Count == 0)
        {
            return new AvailableTablesResponse()
            {
                Success = false,
                Error = "no tables retrived from db " + (tables.Count),
                ErrorCode = "500"
            };
        }

        // check if there is at least one table that can fit all guests
        if (tables.Where(t => t.Seats >= guests).ToList().Count == 0)
        {

            return new AvailableTablesResponse()
            {
                Success = false,
                Error = "couldnt find a table that would fit all guests",
                ErrorCode = "404"
            };
        }

        // results there are tables that could fit guests
        // check if each table can fit guests
        foreach (var t in tables)
        {
            if (t.Seats < guests)
            {
                t.Available = false;
            }
        }

        //retreive overlapping bookings
        var overlapBookings = _context.Bookings.Where(
            b => b.Date.Date == start.Date &&
                 (end > b.StartDate && start < b.EndDate)
        ).ToList();

        // check if table has overlapping bookings
        if (overlapBookings.Count > 0)
        {
            foreach (var t in tables)
            {
                if (overlapBookings.Where(booking => booking.TableId == t.Id).FirstOrDefault() != null)
                {
                    t.Available = false;
                }
            }
        }

        // build response
        var resultTables = new List<AvailableTable>();

        foreach (var t in tables)
        {
            resultTables.Add(new AvailableTable()
            {
                TableId = t.Id,
                TableNo = t.TableNo,
                Available = t.Available
            });
        }

        return new AvailableTablesResponse()
        {
            Success = true,
            AvailableTables = resultTables
        };
    }

    public async Task<UpdateTableResponse> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request)
    {

        // check if table exists
        var table = await _context.Tables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == request.TableId);

        if (table == null)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Couldn't find table with id: " + request.TableId,
                ErrorCode = "404"
            };
        }

        // update booking times
        var times = JsonSerializer.Serialize(request.BookingTimes);
        table.BookingTimes = times;
        _context.Entry(table).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return new UpdateTableResponse()
        {
            Success = true,
            SuccessMessage = "Table updated with booking times"
        };
    }

    public async Task<UpdateTableResponse> UpdateTablesDeadline(long restaurantId, DateTime deadline)
    {

        // check if could find restaurant
        var restaurant = await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.Id == restaurantId);
        if (restaurant == null)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Couldnt find restaurant with id:" + restaurantId,
                ErrorCode = "404"
            };
        }

        // check if deadline is valid
        if (deadline == null || deadline.Hour == 0)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Deadline invalid or hours = 0",
                ErrorCode = "400"
            };
        }

        // retrieve tables
        var tables = await _context.Tables.AsNoTracking().Where(t => t.RestaurantId == restaurantId).ToListAsync();
        foreach (var t in tables)
        {
            t.Deadline = deadline;
            _context.Entry(t).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        return new UpdateTableResponse()
        {
            Success = true,
            SuccessMessage = "Tables updated with new deadline"
        };
    }

    public async Task<UpdateTableResponse> UpdateTableAge(long tableId, bool age)
    {

        // check if table existst
        var table = await _context.Tables.AsNoTracking().Include(t => t.Restriction).FirstOrDefaultAsync(t => t.Id == tableId);

        if (table == null)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Couldn't find table with id: " + tableId,
                ErrorCode = "404"
            };
        }

        // if restrictions exists/not
        var restriction = await _context.Restrictions.AsNoTracking().FirstOrDefaultAsync(r => r.Id == table.RestrictionId);

        if (table.Restriction != null)
        {
            // 2 scenarios
            // Handicap(true) Age(false) -> true, true
            // Handicap(true) Age(true) -> true, false
            if (restriction.Handicap == true)
            {
                restriction = await _context.Restrictions.AsNoTracking().Where(r => r.Age == age && r.Handicap == true).FirstOrDefaultAsync();
                table.RestrictionId = restriction.Id;
                _context.Entry(table).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            // 1 scenario
            // Handicap(false) Age(true) -> false, false
            else
            {
                table.RestrictionId = null;
                _context.Entry(table).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        // 1 scenario
        // Handicap(false) Age(false) -> false, true
        else
        {
            restriction = await _context.Restrictions.AsNoTracking().Where(r => r.Age == age && r.Handicap == false).FirstOrDefaultAsync();
            table.RestrictionId = restriction.Id;
            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        return new UpdateTableResponse()
        {
            Success = true,
            SuccessMessage = "Table age restriction updated"
        };

    }

    public async Task<UpdateTableResponse> UpdateTableHandicap(long tableId, bool handicap)
    {
        // check if table existst
        var table = await _context.Tables.AsNoTracking().Include(t => t.Restriction).FirstOrDefaultAsync(t => t.Id == tableId);

        if (table == null)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Couldn't find table with id: " + tableId,
                ErrorCode = "404"
            };
        }

        // if restrictions exists/not
        var restriction = await _context.Restrictions.AsNoTracking().FirstOrDefaultAsync(r => r.Id == table.RestrictionId);

        if (table.Restriction != null)
        {
            // 2 scenarios
            // Age(true) Handicap(false) -> true, true
            // Age(true) handicap(true) -> true, false
            if (restriction.Age == true)
            {
                restriction = await _context.Restrictions.AsNoTracking().Where(r => r.Handicap == handicap && r.Age == true).FirstOrDefaultAsync();
                table.RestrictionId = restriction.Id;
                _context.Entry(table).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            // 1 scenario
            // Age(false) Handicap(true) -> false, false
            else
            {
                table.RestrictionId = null;
                _context.Entry(table).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        // 1 scenario
        // Age(false) Handicap(false) -> false, true
        else
        {
            restriction = await _context.Restrictions.AsNoTracking().Where(r => r.Handicap == handicap && r.Age == false).FirstOrDefaultAsync();
            table.RestrictionId = restriction.Id;
            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        return new UpdateTableResponse()
        {
            Success = true,
            SuccessMessage = "Table handicap restriction updated"
        };
    }

    public async Task<UpdateTableResponse> UpdateTablesNotes(long tableId, string note)
    {
        // check if could find table
        var table = await _context.Tables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == tableId);
        if (table == null)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Couldnt find restaurant with id:" + tableId,
                ErrorCode = "404"
            };
        }

        // check if note is valid
        if (note == null || note.Length < 1 || note.Length > 200)
        {
            return new UpdateTableResponse()
            {
                Success = false,
                Error = "Note invalid or length = 0 or exceeds 200 characters",
                ErrorCode = "400"
            };
        }


        // update table
        table.Notes = note;
        _context.Entry(table).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return new UpdateTableResponse()
        {
            Success = true,
            SuccessMessage = "Tables updated with new deadline"
        };
    }
}