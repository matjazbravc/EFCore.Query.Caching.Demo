using EFCoreQueryCachingDemo.Models.Dto;

namespace EFCoreQueryCachingDemo.HttpClients
{
    public interface ICityBikesHttpClient
	{
		Task<CityBikesNetworks?> GetCityBikesNetworksAsync(CancellationToken cancellationToken);
	}
}