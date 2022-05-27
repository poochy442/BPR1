namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.Helpers;
public class BusinessLogic : IBusinessLogic
{
    private readonly IUserBL _userBL;
    private readonly ITableBL _tableBL;
    private readonly IBookingBL _bookingBL;
    private readonly IRestaurantBL _restaurantBL;

    public BusinessLogic(
      IUserBL userBL,
      ITableBL tableBL,
      IBookingBL bookingBL,
      IRestaurantBL restaurantBL
      )
    {
        _userBL = userBL;
        _tableBL = tableBL;
        _bookingBL = bookingBL;
        _restaurantBL = restaurantBL;
    }

    public async Task<GetUsersResponse> GetUsers() {
        return await _userBL.GetUsers();
    }

    public async Task<TokenResponse> LoginUser(LoginRequest request) {
        return await _userBL.LoginUser(request);
    }

    public async Task<TokenResponse> LoginManager(LoginRequest request) {
        return await _userBL.LoginManager(request);
    }

    public async Task<RegisterUserResponse> RegisterUser(RegisterRequest request) {
        return await _userBL.RegisterUser(request);
    }

    public async Task<GetTablesResponse> GetTables(long restaurantId) {
        return await _tableBL.GetTables(restaurantId);
    }

    public async Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end) {
        return await _tableBL.GetAvailableTables(restaurantId, guests, start, end);
    }

    public async Task<UpdateTableResponse> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request) {
        return await _tableBL.UpdateTableBookingTimes(request);
    }

    public async Task<UpdateTableResponse> UpdateTablesDeadline(long restaurantId, DateTime deadline) {
        return await _tableBL.UpdateTablesDeadline(restaurantId, deadline);
    }

    public async Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId) {
        return await _bookingBL.GetBookingsForTables(restaurantId);
    }

    public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request) {
        return await _bookingBL.CreateBooking(request);
    }

    public async Task<CreateBookingResponse> CreateInCallBooking(CreateInCallBookingRequest request) {
        return await _bookingBL.CreateInCallBooking(request);
    }

    public async Task<DeleteBookingResponse> DeleteBooking(long id) {
        return await _bookingBL.DeleteBooking(id);
    }

    public async Task<DeleteBookingResponse> CancelBooking(long id) {
        return await _bookingBL.CancelBooking(id);
    }

    public async Task<GetRestaurantsResponse> GetRestaurants(string city) {
        return await _restaurantBL.GetRestaurants(city);
    }

}



