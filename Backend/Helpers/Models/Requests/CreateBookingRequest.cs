namespace Backend.Helpers.Models.Requests;
using System.ComponentModel.DataAnnotations;
public class CreateBookingRequest
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int GuestNo { get; set; }
    public string? Note { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public int TableId { get; set; }
    [Required]
    public int UserId { get; set; }
}