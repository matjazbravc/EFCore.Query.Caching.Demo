using EFCoreQueryCachingDemo.Controllers.Base;
using EFCoreQueryCachingDemo.Services.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace EFCoreQueryCachingDemo.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[EnableCors("EnableCORS")]
	[Produces("application/json")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class RedisCacheController : BaseController<RedisCacheController>
	{
		private readonly IConnectionMultiplexer _connectionMultiplexer;
		private readonly EasyCachingConfig _easyCachingConfig;

		public RedisCacheController(IOptions<EasyCachingConfig> easyCachingConfig, IConnectionMultiplexer connectionMultiplexer)
		{
			_connectionMultiplexer = connectionMultiplexer;
			_easyCachingConfig = easyCachingConfig.Value;
		}

		/// <summary>
		/// Get cached Redis keys with prefix "EF_"
		/// </summary>
		/// <response code="200">Returns list of keys</response>
		[HttpGet("getRedisKeys")]
		public IActionResult GetCachedRedisKeys()
		{
			Logger.LogInformation("Retrieving cached Redis keys with prefix \"EF_\"");
			var keys = _connectionMultiplexer.GetServer(_easyCachingConfig.DbConfig_Endpoint, _easyCachingConfig.DbConfig_Port).Keys(pattern: "EF_*");
			List<string> listKeys = new();
			listKeys.AddRange(keys.Select(key => (string)key).OrderBy(key => key.ToString()));
			return Ok(listKeys);
		}

		/// <summary>
		/// Delete cached Redis keys with prefix "EF_"
		/// </summary>
		/// <response code="200">Success</response>
		[HttpDelete("deleteCachedRedisKeys")]
		public async Task<IActionResult> DeleteCachedRedisKeys()
		{
			Logger.LogInformation("Deleting cached Redis keys with prefix \"EF_\"");
			var keys = _connectionMultiplexer.GetServer(_easyCachingConfig.DbConfig_Endpoint, _easyCachingConfig.DbConfig_Port).Keys(pattern: "EF_*").ToArray();
			await _connectionMultiplexer.GetDatabase().KeyDeleteAsync(keys).ConfigureAwait(false);
			return Ok();
		}
	}
}
