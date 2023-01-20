namespace EFCoreQueryCachingDemo.Services
{
	public interface ICityBikesService
	{
		Task<bool> AddCityBikesNetworksAsync(CancellationToken cancellationToken = default);
	}
}
