using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.DataAccess.DAO_Models;
public class Restriction
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }

	[Column(TypeName = "bit")]
	public bool Age { get; set; }

	[Column(TypeName = "bit")]
	public bool Handicap { get; set; }

}
