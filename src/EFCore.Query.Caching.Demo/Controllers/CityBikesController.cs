using EFCoreQueryCachingDemo.Controllers.Base;
using EFCoreQueryCachingDemo.Models.Dto;
using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Converters;
using EFCoreQueryCachingDemo.Services.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Asp.Versioning;

namespace EFCoreQueryCachingDemo.Controllers;

[ApiVersion("1.0")]
[ApiController]
[EnableCors("EnableCORS")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CityBikesController(
  ICityBikesNetworksRepository repository)
  : BaseController<CityBikesController>
{
  private readonly Stopwatch _stopwatch = new();

  /// <summary>
  /// Search CityBikes network by name
  /// </summary>
  /// <response code="200">Returns list of networks</response>
  [HttpGet("searchCityBikesNetworkAsync")]
  [ProducesResponseType<IEnumerable<NetworkDto>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<NetworkDto>>> SearchCityBikesNetworkAsync(string name)
  {
    _stopwatch.Start();

    IList<Network> networks = await repository.SearchNetworksAsync(name).ConfigureAwait(false);
    List<NetworkDto> result = networks.ToDtoModel();

    _stopwatch.Stop();
    Logger?.LogInformation("Searching for {Name}, time elapsed (ms): {Milliseconds}", name, _stopwatch.ElapsedMilliseconds);

    return Ok(result);
  }

  /// <summary>
  /// Get CityBikes networks
  /// </summary>
  /// <response code="200">Returns list of networks</response>
  [HttpGet("getCityBikesNetworksAsync")]
  [ProducesResponseType<IEnumerable<NetworkDto>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<NetworkDto>>> GetCityBikesNetworksAsync()
  {
    _stopwatch.Start();

    IList<Network> networks = await repository.GetNetworksAsync().ConfigureAwait(false);
    List<NetworkDto> result = networks.ToDtoModel();

    _stopwatch.Stop();
    Logger?.LogInformation("Loading CityBikes networks, time elapsed (ms): {Milliseconds}", _stopwatch.ElapsedMilliseconds);

    return Ok(result);
  }
}
