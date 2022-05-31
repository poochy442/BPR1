namespace Backend.Helpers.Models.Responses;
using Backend.Helpers.Models;

public class GetTableBookingsResponse : BaseResponse
{
    public List<TableBooking>? TableBookings {get; set;}
}