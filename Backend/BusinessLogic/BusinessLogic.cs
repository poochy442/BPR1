namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
public class BusinessLogic : IBusinessLogic
{
    private readonly IUserBL _userBL;
    private readonly ITableBL _tableBL;
    private readonly IBookingBL _bookingBL;

    public BusinessLogic(
      IUserBL userBL,
      ITableBL tableBL,
      IBookingBL bookingBL
      )
    {
        _userBL = userBL;
        _tableBL = tableBL;
        _bookingBL = bookingBL;
    }

    public async Task<TokenResponse> LoginUser(LoginRequest request) {
        return await _userBL.LoginUser(request);
    }

    public async Task<GetTablesResponse> GetTables(long restaurantId) {
        return await _tableBL.GetTables(restaurantId);
    }

    public async Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end) {
        return await _tableBL.GetAvailableTables(restaurantId, guests, start, end);
    }

    public async Task<GetTableBookingsResponse> GetBookingsForTables(long restaurantId) {
        return await _bookingBL.GetBookingsForTables(restaurantId);
    }

    public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request) {
        return await _bookingBL.CreateBooking(request);
    }

}



