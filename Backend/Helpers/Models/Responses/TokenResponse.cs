namespace Backend.Helpers.Models.Responses;
using Backend.DataAccess.Models;
using System.Text.Json.Serialization;

public class TokenResponse : BaseResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }

    public User? User { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Restaurant>? Restaurants { get; set; }
}