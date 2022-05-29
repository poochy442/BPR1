using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DataAccess.Models;

public class Booking
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "int")]
    public int? GuestNo { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Note { get; set; }

    //Relationships
    public Restaurant? Restaurant { get; set; }

    public int? RestaurantId { get; set; }
    public Table? Table { get; set; }

    public int? TableId { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }
}
