using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Restaurant
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
	public long ManagerId { get; set; }
	public string TableIds { get; set; }
	public string Address { get; set; }
	public float Latitude { get; set; }
	public float Longitude { get; set; }
	public string Name { get; set; }
	public string FoodTypes { get; set; }
	public float TotalScore { get; set; }

	public Restaurant(long managerId, string tableIds, string address, float latitude, float longitude, string name, string foodTypes)
	{
		ManagerId = managerId;
		TableIds = tableIds;
		Address = address;
		Latitude = latitude;
		Longitude = longitude;
		Name = name;
		FoodTypes = foodTypes;
		TotalScore = 0;
	}
}
