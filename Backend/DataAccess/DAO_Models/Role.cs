using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.DataAccess.DAO_Models;
public class Role
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string Name { get; set; }

	[Column(TypeName = "varchar(50)")]
	public string Claims { get; set; }

	//Relationship
	//public List<User> Users {get;set;}

}