using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.DataAccess.Models;
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [JsonIgnore]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? PhoneNo { get; set; }

    //Relationship
    public Role? Role { get; set; }
    public int? RoleId { get; set; }
}