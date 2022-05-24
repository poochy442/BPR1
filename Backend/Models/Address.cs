using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Address
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }
	[Column(TypeName = "varchar(50)")]
	public string PostalCode { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string City { get; set; }
	
	[Column(TypeName = "varchar(50)")]
	public string Street { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string? StreetNo { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string Location { get; set; }
	
	//Relationships

}
