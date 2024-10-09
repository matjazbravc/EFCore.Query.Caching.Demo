using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Repositories.Base;

namespace EFCoreQueryCachingDemo.Services.Repositories;

public interface ICityBikesNetworksRepository : IBaseRepository<Network>
{
  Task<IList<Network>> SearchNetworksAsync(string name, bool disableTracking = true, CancellationToken cancellationToken = default);

  Task<IList<Network>> GetNetworksAsync(bool disableTracking = true, CancellationToken cancellationToken = default);
}
