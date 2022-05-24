using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DataAccess.Models;

public class Restaurant
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }
	[Column(TypeName = "varchar(50)")]
	public string Name { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string? FoodType { get; set; }
	
	[Column(TypeName = "int")]
	public int? StudentDiscount { get; set; }

	[Column(TypeName = "varchar(400)")]
	public string? WorkingHours { get; set; }

	[Column(TypeName = "decimal(5,2)")]
	public float? TotalScore { get; set; }

	//Relationships

	// better not to create lists -> it will create object loop
	public List<Booking> Bookings{get;set;}
	public List<Table> Tables{get;set;}
	public User? User {get;set;}
	public Address Address {get; set;}
	
}