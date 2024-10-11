using Asp.Versioning;
using EFCoreQueryCachingDemo.Controllers.Base;
using EFCoreQueryCachingDemo.Services.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace EFCoreQueryCachingDemo.Controllers;

[ApiVersion("1.0")]
[ApiController]
[EnableCors("EnableCORS")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RedisCacheController(
  IOptions<EasyCachingConfig> easyCachingConfig,
  IConnectionMultiplexer connectionMultiplexer)
  : BaseController<RedisCacheController>
{
  private readonly EasyCachingConfig _easyCachingConfig = easyCachingConfig.Value;

  /// <summary>
  /// Get cached Redis keys with prefix "EF_"
  /// </summary>
  /// <response code="200">Returns list of keys</response>
  [HttpGet("getRedisKeys")]
  [ProducesResponseType<int>(StatusCodes.Status200OK)]
  public IActionResult GetCachedRedisKeys()
  {
    Logger?.LogInformation("Retrieving cached Redis keys with prefix \"EF_\"");
    var keys = connectionMultiplexer.GetServer(_easyCachingConfig.DbConfig_Endpoint, _easyCachingConfig.DbConfig_Port).Keys(pattern: "EF_*");
    List<string> listKeys = [.. keys.Select(key => (string)key).OrderBy(key => key.ToString())];
    return Ok(listKeys);
  }

  /// <summary>
  /// Delete cached Redis keys with prefix "EF_"
  /// </summary>
  /// <response code="200">Success</response>
  [HttpDelete("deleteCachedRedisKeys")]
  [ProducesResponseType<int>(StatusCodes.Status200OK)]
  public async Task<IActionResult> DeleteCachedRedisKeys()
  {
    Logger?.LogInformation("Deleting cached Redis keys with prefix \"EF_\"");
    var keys = connectionMultiplexer.GetServer(_easyCachingConfig.DbConfig_Endpoint, _easyCachingConfig.DbConfig_Port).Keys(pattern: "EF_*").ToArray();
    await connectionMultiplexer.GetDatabase().KeyDeleteAsync(keys).ConfigureAwait(false);
    return Ok();
  }
}
