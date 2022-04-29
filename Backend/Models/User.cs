using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models;
public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	[JsonIgnore]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	public string Name { get; set; }
	public string PhoneNo { get; set; }

	public User(string email, string password, string name, string phoneNo)
	{
		Email = email;
		Password = password;
		Name = name;
		PhoneNo = phoneNo;
	}
}