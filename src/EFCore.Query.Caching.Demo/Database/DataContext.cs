using EFCoreQueryCachingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQueryCachingDemo.Database;

/// <summary>
/// Database context
/// </summary>
public class DataContext(DbContextOptions<DataContext> options)
  : DbContext(options)
{
  public DbSet<Network> CityBikesNetworks { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    // Create EF entities and relations
    _ = new NetworkConfiguration(modelBuilder.Entity<Network>());
    _ = new LocationConfiguration(modelBuilder.Entity<Location>());
  }
}
