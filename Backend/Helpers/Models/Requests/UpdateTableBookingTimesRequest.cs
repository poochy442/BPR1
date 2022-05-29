namespace Backend.Helpers.Models.Requests;
using Backend.Helpers;
using System.ComponentModel.DataAnnotations;
public class UpdateTableBookingTimesRequest
{
    [Required]
    public long TableId { get; set; }
    [Required]
    [MinLength(1)]
    public List<WorkingHours> BookingTimes { get; set; }
}