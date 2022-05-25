using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models;

public enum Role {
	Admin,
	RestaurantManager,
	Customer
}
public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	[JsonIgnore]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string Name { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string PhoneNo { get; set; }

	[EnumDataType(typeof(Role))]
	public Role Role {get;set;}
}