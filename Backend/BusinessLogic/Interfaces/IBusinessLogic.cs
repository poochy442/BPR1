namespace Backend.BusinessLogic;

using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using System.Security.Claims;

public interface IBusinessLogic
{
    Task<GetUsersResponse> GetUsers();
    Task<TokenResponse> LoginUser(LoginRequest request);
    Task<TokenResponse> AutoLogin(int userId, Claim claims);
    Task<RegisterUserResponse> RegisterUser(RegisterRequest request);
    Task<GetTablesResponse> GetTables(long restaurantId);
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
    Task<UpdateTableResponse> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request);
    Task<UpdateTableResponse> UpdateTablesDeadline(long restaurantId, DateTime deadline);
    Task<UpdateTableResponse> UpdateTableAge(long tableId, bool age);
    Task<UpdateTableResponse> UpdateTableHandicap(long tableId, bool handicap);
    Task<UpdateTableResponse> UpdateTablesNotes(long tableId, string note);
    Task<GetBookingsResponse> GetCurrentBookingsForCustomer(int customerId);
    Task<GetBookingsResponse> GetPastBookingsForCustomer(int customerId);
    Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId);
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
    Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request);
    Task<DeleteBookingResponse> DeleteBooking(long id);
    Task<DeleteBookingResponse> CancelBooking(long id);
    Task<GetRestaurantsResponse> GetRestaurant(long id);
    Task<GetRestaurantsResponse> GetRestaurants(string city);
    Task<GetRestaurantsResponse> GetRestaurantsByLocation(RestaurantsByLocationRequest request);
}