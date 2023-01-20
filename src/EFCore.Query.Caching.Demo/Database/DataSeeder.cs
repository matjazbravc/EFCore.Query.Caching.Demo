using EFCoreQueryCachingDemo.Services;

namespace EFCoreQueryCachingDemo.Database
{
	public class DataSeeder : IDataSeeder
	{
		private readonly DataContext _context;
		private readonly ICityBikesService _cityBikesService;

		public DataSeeder(DataContext context, ICityBikesService cityBikesService)
		{
			_context = context;
			_cityBikesService = cityBikesService;
		}

		/// <summary>
		/// If the database is empty, fill it with data (CityBikes Networks)
		/// </summary>
		/// <returns></returns>
		public async Task InitializeAsync()
		{
			await _context.Database.EnsureCreatedAsync().ConfigureAwait(false);

			if (_context.CityBikesNetworks.Any() && _context.CityBikesNetworks.Any())
			{
				return;
			}
			
			await _cityBikesService.AddCityBikesNetworksAsync().ConfigureAwait(false);
		}
	}
}