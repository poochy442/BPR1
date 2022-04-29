using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Booking
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
    public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public int GuestNo { get; set; }

	public Booking(DateTime startDate, DateTime endDate, int guestNo)
	{
		StartDate = startDate;
		EndDate = endDate;
		GuestNo = guestNo;
	}
}
