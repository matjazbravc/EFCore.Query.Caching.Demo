using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Repositories.Base;

namespace EFCoreQueryCachingDemo.Services.Repositories
{
	public interface ICityBikesNetworksRepository : IBaseRepository<Network>
	{
		Task<IList<Network>> SearchNetworksAsync(string name, CancellationToken cancellationToken = default, bool disableTracking = true);

		Task<IList<Network>> GetNetworksAsync(CancellationToken cancellationToken = default, bool disableTracking = true);
	}
}
