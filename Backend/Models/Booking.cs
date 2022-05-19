using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Booking
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
	[Required]
	public DateTime Date {get; set;}
    public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }

	[Column(TypeName = "int")]
	public int? GuestNo { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string? Note {get; set;}

	//Relationships
	public Restaurant Restaurant {get; set;}
	public Table Table{get; set;}
	public User User{get;set;}

	/*public Booking(DateTime date, DateTime startDate, DateTime endDate, int guestNo, string note)
	{
		Date = date;
		StartDate = startDate;
		EndDate = endDate;
		GuestNo = guestNo;
		Note = note;
	}*/
}
