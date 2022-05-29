namespace Backend.Helpers;
using System.ComponentModel.DataAnnotations;

[Serializable]
public class WorkingHours
{
    [Required]
    public int Day {get; set;}
    [Required]
    public string? From {get; set;}
    [Required]
    public string? Till {get; set;}
}