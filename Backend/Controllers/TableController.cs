using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Backend.Helpers;
using Backend.DataAccess.Models;
using Backend.DataAccess;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.BusinessLogic;

namespace Backend.Controllers;

[EnableCors]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class TableController : ControllerBase
{
    private readonly DBContext _context;
    private readonly IBusinessLogic _businessLogic;

    public TableController(DBContext context, IBusinessLogic businessLogic)
    {
        _context = context;
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
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [AllowAnonymous]
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