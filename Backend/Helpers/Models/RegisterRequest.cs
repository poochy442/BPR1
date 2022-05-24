using System.ComponentModel.DataAnnotations;

namespace Backend.Helpers.Models;
public class RegisterRequest
{
    [Required]
	public string? Email { get; set; }

    [Required]
	public string? Password { get; set; }
	
    [Required]
	public string? Name { get; set; }
	
    [Required]
	public string? PhoneNo { get; set; }
}