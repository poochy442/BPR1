namespace Backend.BusinessLogic;

using Microsoft.AspNetCore.Mvc;
using Backend.Helpers.Models.Requests;
using Backend.Helpers.Models.Responses;

public interface IBookingBL
{
    Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);
}