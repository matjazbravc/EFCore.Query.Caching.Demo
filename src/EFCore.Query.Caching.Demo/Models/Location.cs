using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreQueryCachingDemo.Models;

public class Location
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Location_Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(255)")]
  public string City { get; set; }

  [Column(TypeName = "varchar(50)")]
  public string? Country { get; set; }

  public double? Latitude { get; set; }

  public double? Longitude { get; set; }

  // Navigation properties
  public int Network_Id { get; set; } // Foreign key

  public Network Network { get; set; }
}
