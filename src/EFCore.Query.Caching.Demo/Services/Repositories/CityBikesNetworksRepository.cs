using EFCoreQueryCachingDemo.Database;
using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQueryCachingDemo.Services.Repositories
{
	public class CityBikesNetworksRepository : BaseRepository<Network, DataContext>, ICityBikesNetworksRepository
	{

		public CityBikesNetworksRepository(DataContext dbContext)
			: base(dbContext)
		{
		}

		public async Task<IList<Network>> GetNetworksAsync(CancellationToken cancellationToken = default, bool disableTracking = true)
		{
			var result = await GetAsync<Network>(
				include: source => source
					.Include(cmp => cmp.Location),
				orderBy: cmp => cmp
					.OrderBy(o => o.Name),
				tracking: disableTracking, cancellationToken: cancellationToken).ConfigureAwait(false);
			return result;
		}

		public async Task<IList<Network>> SearchNetworksAsync(string name, CancellationToken cancellationToken = default, bool disableTracking = true)
		{
			IList<Network> result = new List<Network>();
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
}
