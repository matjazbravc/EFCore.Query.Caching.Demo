using System.Text.Json;
using StackExchange.Redis;

namespace EFCoreQueryCachingDemo.Services
{
	public class RedisCacheService : IRedisCacheService
	{
		private readonly IDatabase _database;
		private static IConnectionMultiplexer _connectionMultiplexer;

		public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
		{
			_connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
			_database = _connectionMultiplexer.GetDatabase(0);
		}

		public async Task<T> GetAsync<T>(string key)
		{
			var redisKey = new RedisKey(key);
			var redisValue = await _database.StringGetAsync(redisKey);
			if (redisValue.HasValue)
			{
				T? value = JsonSerializer.Deserialize<T>(redisValue.ToString());
				return value;
			}
			return default;
		}

		public async Task<bool> RemoveAsync(string key)
		{
			RedisKey redisKey = new(key);
			return await _database.KeyDeleteAsync(redisKey);
		}

		public async Task<bool> SetAsync<T>(string key, T value, int expiredInMinute = 120)
		{
			RedisKey redisKey = new(key);
			var valueStr = JsonSerializer.Serialize(value);
			RedisValue redisValue = new(valueStr);
			return await _database.StringSetAsync(redisKey, redisValue, TimeSpan.FromMinutes(expiredInMinute));
		}

		public async Task<TimeSpan> PingAsync()
		{
			return await _database.PingAsync();
		}
	}
}
