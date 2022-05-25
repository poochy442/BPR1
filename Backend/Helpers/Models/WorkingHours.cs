namespace Backend.Helpers;

[Serializable]
public class WorkingHours
{
    public int Day {get; set;}
    public string? From {get; set;}
    public string? Till {get; set;}
}