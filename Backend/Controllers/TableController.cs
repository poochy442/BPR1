using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

using Backend.DataAccess.DAO_Models;
using Backend.DataAccess;
using Backend.Helpers;

namespace Backend.Controllers;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class TableController : ControllerBase
{
    private readonly DBContext _context;

    public TableController(DBContext context)
    {
        _context = context;
    }

    // [HttpGet]
    // public async Task<ActionResult<List<Table>>> GetTables(long restaurantId)
    // {
    // 	var r = await _context.Restaurants.FindAsync(restaurantId);
    // 	if(r == null) return NotFound();

    // 	List<long> ids = JsonSerializer.Deserialize<List<long>>(r.TableIds) ?? new List<long>();
    // 	List<Table> tables = _context.Tables.ToList().FindAll((element) => ids.Contains(element.Id));
    //     return tables;
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<Table>> GetTable(long id)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null) return NotFound();

        return table;
    }

    [HttpGet]
    [Route("available-tables")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAvailableTables([FromQuery] long restaurantId, int guests, DateTime start, DateTime end)
    {
        // find restaurant
        var restaurant = await _context.Restaurants.FindAsync((int)restaurantId);

        if (restaurant != null)
        {

            // extract restaurant's working hours
            var restaurantHours = JsonSerializer.Deserialize<List<WorkingHours>>(restaurant.WorkingHours);

            // find tables suited for guests No
            var tables = _context.Tables.Where(t => t.Seats >= guests).ToList();
            if (tables.Count >= 0)
            {
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

                if (start >= open && end <= close)
                {
                    return Ok("possible");
                }
                return BadRequest("time period doesnt fit in working hours of restaurant " +
                open + "  " + close);

            }

            return NotFound("Couldn't find table that would fit guests");


        }

        return NotFound("Couldnt find restaurant with id:" + restaurantId);
    }

    // [HttpPost]
    // public async Task<ActionResult<Restaurant>> PostTable(long restaurantId, [FromBody] Table table)
    // {
    // 	var r = await _context.Restaurants.FindAsync(restaurantId);
    // 	if(r == null) return NotFound();

    // 	_context.Tables.Add(table);
    // 	await _context.SaveChangesAsync();

    // 	List<long> newIds = JsonSerializer.Deserialize<List<long>>(r.TableIds) ?? new List<long>();
    // 	newIds.Add(table.Id);
    // 	r.TableIds = JsonSerializer.Serialize(newIds);
    // 	await _context.SaveChangesAsync();

    // 	return CreatedAtAction(
    // 		nameof(GetTable),
    // 		new { id = table.Id },
    // 		table
    // 	);
    // }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTable(long id, Table table)
    {
        if (id != table.Id)
        {
            return BadRequest();
        }

        var putTable = await _context.Tables.FindAsync(table.Id);
        if (putTable == null)
        {
            return NotFound();
        }

        putTable = table;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TableExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    private bool TableExists(long id)
    {
        return _context.Tables.Any(e => e.Id == id);
    }

}