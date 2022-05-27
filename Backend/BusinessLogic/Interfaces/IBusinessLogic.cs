namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface IBusinessLogic
{
    Task<GetUsersResponse> GetUsers();
    Task<TokenResponse> LoginUser(LoginRequest request);
    Task<TokenResponse> LoginManager(LoginRequest request);
    Task<RegisterUserResponse> RegisterUser(RegisterRequest request);
    Task<GetTablesResponse> GetTables(long restaurantId);
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
    Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId);
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
    Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request);
    Task<DeleteBookingResponse> DeleteBooking(long id);
    Task<GetRestaurantsResponse> GetRestaurants(string city);
}