namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using System.Text;

public interface IBookingBL
{
    Task<GetBookingsResponse> GetBookingsForCustomer(int customerId);
    Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId);
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
    Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request);
    Task<DeleteBookingResponse> DeleteBooking(long id);
    Task<DeleteBookingResponse> CancelBooking(long id);
}