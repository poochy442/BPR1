namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;
using Backend.Helpers;

public interface ITableBL
{
    Task<GetTablesResponse> GetTables(long restaurantId);
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
    Task<UpdateTableResponse> UpdateTableBookingTimes(UpdateTableBookingTimesRequest request);
    Task<UpdateTableResponse> UpdateTablesDeadline(long restaurantId, DateTime deadline);
    Task<UpdateTableResponse> UpdateTableAge(long tableId, bool age);
    Task<UpdateTableResponse> UpdateTableHandicap(long tableId, bool handicap);
    Task<UpdateTableResponse> UpdateTablesNotes(long tableId, string note);
}