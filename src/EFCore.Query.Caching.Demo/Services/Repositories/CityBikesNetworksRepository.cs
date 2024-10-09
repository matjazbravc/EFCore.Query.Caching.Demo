using EFCoreQueryCachingDemo.Database;
using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQueryCachingDemo.Services.Repositories;

public class CityBikesNetworksRepository(
  DataContext dbContext)
  : BaseRepository<Network, DataContext>(dbContext), ICityBikesNetworksRepository
{
  public async Task<IList<Network>> GetNetworksAsync(bool disableTracking = true, CancellationToken cancellationToken = default)
  {
    IList<Network> result = await GetAsync<Network>(
      include: source => source
        .Include(cmp => cmp.Location),
      orderBy: cmp => cmp
        .OrderBy(o => o.Name),
      tracking: disableTracking, cancellationToken: cancellationToken).ConfigureAwait(false);
    return result;
  }

  public async Task<IList<Network>> SearchNetworksAsync(string name, bool disableTracking = true, CancellationToken cancellationToken = default)
  {
    IList<Network> result = [];
    if (!string.IsNullOrEmpty(name))
    {
      result = await GetAsync<Network>(
        include: source => source
          .Include(cmp => cmp.Location),
        predicate: e => e.Name.Contains(name),
        orderBy: cmp => cmp
          .OrderBy(o => o.Name),
        tracking: disableTracking, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    return result;
  }
}