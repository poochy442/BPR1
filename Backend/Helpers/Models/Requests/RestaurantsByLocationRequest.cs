using System.ComponentModel.DataAnnotations;

namespace Backend.Helpers.Models.Requests;
public class RestaurantsByLocationRequest
{
    [Required]
    [MinLength(2)]
    public string Country { get; set; }
    [Required]
    [MinLength(4)]
    public string PostalCode { get; set; }
    [Required]
    [MinLength(2)]
    public string City { get; set; }
    [Required]
    [MinLength(4)]
    public string Street { get; set; }
    [Required]
    [MinLength(1)]
    public string StreetNo { get; set; }
    [Required]
    public int Radius { get; set; }
}