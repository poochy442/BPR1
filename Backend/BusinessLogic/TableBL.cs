namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
}