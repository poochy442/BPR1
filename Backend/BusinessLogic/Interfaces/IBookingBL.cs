namespace Backend.BusinessLogic;

using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface IBookingBL
{
    Task<GetBookingsResponse> GetCurrentBookingsForCustomer(int customerId);
    Task<GetBookingsResponse> GetPastBookingsForCustomer(int customerId);
    Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId);
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
    Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request);
    Task<DeleteBookingResponse> DeleteBooking(long id);
    Task<DeleteBookingResponse> CancelBooking(long id);
}