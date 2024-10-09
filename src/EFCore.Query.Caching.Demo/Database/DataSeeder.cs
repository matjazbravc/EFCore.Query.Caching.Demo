using EFCoreQueryCachingDemo.Services;

namespace EFCoreQueryCachingDemo.Database;

public class DataSeeder(DataContext context, ICityBikesService cityBikesService) : IDataSeeder
{

  /// <summary>
  /// If the database is empty, fill it with data (CityBikes Networks)
  /// </summary>
  /// <returns></returns>
  public async Task InitializeAsync()
  {
    await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
    if (context.CityBikesNetworks.Any())
    {
      return;
    }
    await cityBikesService.AddCityBikesNetworksAsync().ConfigureAwait(false);
  }
}