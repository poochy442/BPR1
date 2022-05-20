using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;
public class Rating
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }
	public int Score { get; set; }
	
	[Column(TypeName = "varchar(200)")]
	public string Comment { get; set; }

	//Relationships

	public Restaurant  Restaurant{get;set;}
	public User User {get;set;}

	/*public Rating(long restaurantId, int score, string comment)
	{
		RestaurantId = restaurantId;
		Score = score;
		Comment = comment;
	}*/
}
