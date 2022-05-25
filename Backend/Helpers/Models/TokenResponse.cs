namespace Backend.Helpers.Models;
using Backend.Models;

public class TokenResponse : BaseResponse
{
    public string Token { get; set; }
	public User User { get; set; }
}