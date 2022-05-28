namespace Backend.Helpers.Models.Responses;
using Backend.Helpers.Models.Responses;
using Backend.DataAccess.Models;

public class GetBookingsResponse : BaseResponse
{
    public List<Booking>? Bookings { get; set; }
}