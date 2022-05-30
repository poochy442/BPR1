using Backend.Helpers.Models.Responses;
namespace Backend.Helpers.Models.Responses;
public class RegisterUserResponse : BaseResponse
{
    public string? SuccessMessage { get; set; }
    public TokenResponse TokenResponse { get; set; }
}