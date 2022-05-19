using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;
public class Table
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
	public int TableNo { get; set; }
	public int Seats { get; set; }
	public bool Available { get; set; }
	public string TableNote { get; set; }
	public string Restrictions { get; set; }

	/*public Table(int tableNo, int seats, bool available, string tableNote, string restrictions)
	{
		TableNo = tableNo;
		Seats = seats;
		Available = available;
		TableNote = tableNote;
		Restrictions = restrictions;
	}*/
}
