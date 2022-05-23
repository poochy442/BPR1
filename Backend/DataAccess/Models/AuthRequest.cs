// using System.ComponentModel.DataAnnotations;

// namespace Backend.DataAccess.Models;

// public class AuthenticateRequest
// {
// 	[Required]
// 	public string Email { get; set; }
// 	[Required]
// 	public string Password { get; set; }
// }

// public class RegisterRequest
// {
// 	[Required]
// 	public string Email { get; set; }
// 	[Required]
// 	public string Password { get; set; }
// 	[Required]
// 	public string Name { get; set; }
// 	[Required]
// 	public string PhoneNo { get; set; }
// }

// public class AuthenticationResponse
// {
// 	public long Id { get; set; }
// 	public string Email { get; set; }
//     public string Name { get; set; }
//     public string PhoneNo { get; set; }
// 	public string Token { get; set; }
// 	public AuthenticationResponse(User user, string token)
// 	{
// 		Id = user.Id;
// 		Email = user.Email;
// 		Name = user.Name;
// 		PhoneNo = user.PhoneNo;
// 		Token = token;
// 	}
// }