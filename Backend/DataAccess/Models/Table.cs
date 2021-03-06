using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DataAccess.Models;
public class Table
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int TableNo { get; set; }
    public int Seats { get; set; }

    [Column(TypeName = "bit")]
    public bool Available { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string? Notes { get; set; }
    public DateTime? Deadline { get; set; }

    [Column(TypeName = "varchar(400)")]
    public string? BookingTimes { get; set; }

    //Relationships

    public Restaurant? Restaurant { get; set; }

    public int? RestaurantId { get; set; }
    public Restriction? Restriction { get; set; }
    public int? RestrictionId { get; set; }
}
