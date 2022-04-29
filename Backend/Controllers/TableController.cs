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
public class TableController : ControllerBase
{
	private readonly DBContext _context;

	public TableController(DBContext context)
	{
		_context = context;
	}

	[HttpGet]
    public async Task<ActionResult<List<Table>>> GetTables(long restaurantId)
    {
		var r = await _context.Restaurants.FindAsync(restaurantId);
		if(r == null) return NotFound();

		List<long> ids = JsonSerializer.Deserialize<List<long>>(r.TableIds) ?? new List<long>();
		List<Table> tables = _context.Tables.ToList().FindAll((element) => ids.Contains(element.Id));
        return tables;
    }

	[HttpGet("{id}")]
	public async Task<ActionResult<Table>> GetTable(long id)
	{
		var table = await _context.Tables.FindAsync(id);

		if(table == null) return NotFound();

		return table;
	}

	[HttpPost]
	public async Task<ActionResult<Restaurant>> PostTable(long restaurantId, [FromBody] Table table)
	{
		var r = await _context.Restaurants.FindAsync(restaurantId);
		if(r == null) return NotFound();

		_context.Tables.Add(table);
		await _context.SaveChangesAsync();

		List<long> newIds = JsonSerializer.Deserialize<List<long>>(r.TableIds) ?? new List<long>();
		newIds.Add(table.Id);
		r.TableIds = JsonSerializer.Serialize(newIds);
		await _context.SaveChangesAsync();

		return CreatedAtAction(
			nameof(GetTable),
			new { id = table.Id },
			table
		);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutTable(long id, Table table)
	{
		if(id != table.Id)
		{
			return BadRequest();
		}

		var putTable = await _context.Tables.FindAsync(table.Id);
		if(putTable == null)
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