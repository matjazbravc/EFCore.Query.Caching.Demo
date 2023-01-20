using EFCoreQueryCachingDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreQueryCachingDemo.Database
{
	public class LocationConfiguration
	{
		public LocationConfiguration(EntityTypeBuilder<Location> entity)
		{
			// Table
			entity.ToTable("Locations");
			
			// Indexes
			entity.HasIndex(b => b.City);
		}
	}
}
