using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;
public class Rating
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
	public long RestaurantId { get; set; }
	public int Score { get; set; }
	public string Comment { get; set; }

	public Rating(long restaurantId, int score, string comment)
	{
		RestaurantId = restaurantId;
		Score = score;
		Comment = comment;
	}
}
