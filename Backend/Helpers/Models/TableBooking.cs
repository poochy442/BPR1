namespace Backend.Helpers.Models;
using Backend.DataAccess.Models;
public class TableBooking
{
    public int TableNo {get; set;}
    public List<Booking>? Bookings {get; set;}
}