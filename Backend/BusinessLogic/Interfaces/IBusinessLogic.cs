namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.Helpers;

public interface IBusinessLogic
{
    Task<GetUsersResponse> GetUsers();
    Task<TokenResponse> LoginUser(LoginRequest request);
    Task<TokenResponse> LoginManager(LoginRequest request);
    Task<RegisterUserResponse> RegisterUser(RegisterRequest request);
    Task<GetTablesResponse> GetTables(long restaurantId);
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
    Task<UpdateTableResponse> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request);
    Task<UpdateTableResponse> UpdateTablesDeadline(long restaurantId, DateTime deadline);
    Task<GetBookingsResponse> GetBookingsForCustomer(int customerId);
    Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId);
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
    Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request);
    Task<DeleteBookingResponse> DeleteBooking(long id);
    Task<DeleteBookingResponse> CancelBooking(long id);
    Task<GetRestaurantsResponse> GetRestaurants(string city);
}