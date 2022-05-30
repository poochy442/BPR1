using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Backend.Helpers.Models;
using Backend.DataAccess.Models;
using Backend.Helpers.Models.Requests;
using Backend.BusinessLogic;

namespace Backend.Controllers;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class TableController : ControllerBase
{
    private readonly IBusinessLogic _businessLogic;

    public TableController(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }


    // [HttpGet("{id}")]
    // public async Task<ActionResult<Table>> GetTable(long id)
    // {
    //     var table = await _context.Tables.FindAsync(id);

    //     if (table == null) return NotFound();

    //     return table;
    // }

    // manager
    [HttpGet("tables")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult<List<Table>>> GetTables(long restaurantId)
    {
        var tables = await _businessLogic.GetTables(restaurantId);

        if (!tables.Success)
        {
            return Unauthorized(new
            {
                tables.ErrorCode,
                tables.Error
            });
        }

        return Ok(tables);

    }

    // both customer and manager
    [HttpGet("available-tables")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAvailableTables([FromQuery] long restaurantId, int guests, DateTime start, DateTime end)
    {
        var availableTables = await _businessLogic.GetAvailableTables(restaurantId, guests, start, end);

        if (!availableTables.Success)
        {
            return Unauthorized(
                new
                {
                    availableTables.ErrorCode,
                    availableTables.Error
                }
            );
        }

        return Ok(availableTables);
    }

    // manager
    [HttpPut("update-booking-times")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request)
    {

        var updateBookingTimes = await _businessLogic.UpdateTableBookingTimes(request);

        if (!updateBookingTimes.Success)
        {
            return Unauthorized(new
            {
                updateBookingTimes.ErrorCode,
                updateBookingTimes.Error
            });
        }

        return Ok(updateBookingTimes);
    }

    // manager
    [HttpPut("update-deadline")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> UpdateTablesDeadline(long restaurantId, DateTime deadline)
    {
        var updateDeadline = await _businessLogic.UpdateTablesDeadline(restaurantId, deadline);

        if (!updateDeadline.Success)
        {
            return Unauthorized(new
            {
                updateDeadline.ErrorCode,
                updateDeadline.Error
            });
        }

        return Ok(updateDeadline);
    }

    // manager
    [HttpPut("update-age")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> UpdateTableAge(long tableId, bool age)
    {
        var updateAge = await _businessLogic.UpdateTableAge(tableId, age);

        if (!updateAge.Success)
        {
            return Unauthorized(new
            {
                updateAge.ErrorCode,
                updateAge.Error
            });
        }

        return Ok(updateAge);
    }

    // manager
    [HttpPut("update-handicap")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> UpdateTableHandicap(long tableId, bool handicap)
    {
        var updateHandicap = await _businessLogic.UpdateTableHandicap(tableId, handicap);

        if (!updateHandicap.Success)
        {
            return Unauthorized(new
            {
                updateHandicap.ErrorCode,
                updateHandicap.Error
            });
        }

        return Ok(updateHandicap);
    }

    // manager
    [HttpPut("update-notes")]
    [Authorize(Roles = UserRoles.RestaurantManager)]
    public async Task<ActionResult> UpdateTablesNotes(long tableId, string note)
    {
        var updateNotes = await _businessLogic.UpdateTablesNotes(tableId, note);

        if (!updateNotes.Success)
        {
            return Unauthorized(new
            {
                updateNotes.ErrorCode,
                updateNotes.Error
            });
        }

        return Ok(updateNotes);
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

    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutTable(long id, Table table)
    // {
    //     if (id != table.Id)
    //     {
    //         return BadRequest();
    //     }

    //     var putTable = await _context.Tables.FindAsync(table.Id);
    //     if (putTable == null)
    //     {
    //         return NotFound();
    //     }

    //     putTable = table;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException) when (!TableExists(id))
    //     {
    //         return NotFound();
    //     }

    //     return NoContent();
    // }

    // private bool TableExists(long id)
    // {
    //     return _context.Tables.Any(e => e.Id == id);
    // }

}