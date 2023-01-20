using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreQueryCachingDemo.Models
{
	public class Network
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Network_Id { get; set; }

		[Required]
		[Column(TypeName = "varchar(255)")]
		public string Name { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? Company { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? Href { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? Id { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? Source { get; set; }

		public Location Location { get; set; }
	}
}
