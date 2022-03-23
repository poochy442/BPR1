using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers;

[EnableCors]
[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
	[HttpGet(Name = "GetBookings")]
    public IEnumerable<Booking> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Booking
        {
            StartDate = DateTime.Now.AddDays(index),
			EndDate = DateTime.Now.AddDays(index).AddHours(2),
            GuestNo = 4
        })
        .ToArray();
    }
}