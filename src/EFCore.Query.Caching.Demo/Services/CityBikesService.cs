using EFCoreQueryCachingDemo.HttpClients;
using EFCoreQueryCachingDemo.Services.Converters;
using EFCoreQueryCachingDemo.Services.Repositories;

namespace EFCoreQueryCachingDemo.Services
{
	public class CityBikesService : ICityBikesService
	{
		private readonly ILogger<CityBikesService> _logger;
		private readonly ICityBikesHttpClient _cityBikesHttpClient;
		private readonly ICityBikesNetworksRepository _cityBikesNetworksRepository;

		public CityBikesService(ILogger<CityBikesService> logger,
			ICityBikesHttpClient cityBikesHttpClient,
			ICityBikesNetworksRepository cityBikesNetworksRepository)
		{
			_logger = logger;
			_cityBikesHttpClient = cityBikesHttpClient;
			_cityBikesNetworksRepository = cityBikesNetworksRepository;
		}

		public async Task<bool> AddCityBikesNetworksAsync(CancellationToken cancellationToken = default)
		{
			bool result;
			var cityBikesResponse = await _cityBikesHttpClient.GetCityBikesNetworksAsync(cancellationToken).ConfigureAwait(false);
			if (cityBikesResponse?.Networks == null)
			{
				_logger.LogInformation("WARNING: Import CityBikes Networks to the database has failed!");
				result = false;
			}
			else
			{
				_logger.LogInformation("Imported {network_count} CityBikes Networks to the database. The site is experiencing technical difficulties and it is unavailable.", cityBikesResponse.Networks.Count);
				await _cityBikesNetworksRepository.AddAsync(cityBikesResponse.Networks.ToEfCoreModel(), cancellationToken).ConfigureAwait(false);
				result = true;
			}	
			return result;
		}
	}
}
