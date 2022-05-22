using System.ComponentModel.DataAnnotations;
public class RegisterModel
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