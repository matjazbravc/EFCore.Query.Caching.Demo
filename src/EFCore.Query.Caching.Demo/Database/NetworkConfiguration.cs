using EFCoreQueryCachingDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreQueryCachingDemo.Database
{
	public class NetworkConfiguration
	{
		public NetworkConfiguration(EntityTypeBuilder<Network> entity)
		{
			// Table
			entity.ToTable("Networks");
			
			// Indexes
			entity.HasIndex(b => b.Name);
			
			// Relationships
			entity.HasOne(a => a.Location)
				.WithOne(b => b.Network)
				.HasForeignKey<Location>(e => e.Network_Id);
		}
	}
}
