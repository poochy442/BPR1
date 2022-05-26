namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface ITableBL
{
    Task<AvailableTablesResponse> GetAvailableTables(long restaurantId, int guests, DateTime start, DateTime end);
}