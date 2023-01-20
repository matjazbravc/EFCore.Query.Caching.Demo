using EFCoreQueryCachingDemo.Controllers.Base;
using EFCoreQueryCachingDemo.Models.Dto;
using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Services.Converters;
using EFCoreQueryCachingDemo.Services.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFCoreQueryCachingDemo.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[EnableCors("EnableCORS")]
	[Produces("application/json")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class CityBikesController : BaseController<CityBikesController>
	{
		private readonly Stopwatch _stopwatch;
		private readonly ICityBikesNetworksRepository _repository;

		public CityBikesController(ICityBikesNetworksRepository repository)
		{
			_repository = repository;
			_stopwatch = new Stopwatch();
		}

		/// <summary>
		/// Search CityBikes network by name
		/// </summary>
		/// <response code="200">Returns list of networks</response>
		[HttpGet("searchCityBikesNetworkAsync")]
		public async Task<ActionResult<IEnumerable<NetworkDto>>> SearchCityBikesNetworkAsync(string name)
		{
			_stopwatch.Start();
			
			IList<Network> networks = await _repository.SearchNetworksAsync(name).ConfigureAwait(false);
			List<NetworkDto> result = networks.ToDtoModel();
			
			_stopwatch.Stop();
			Logger.LogInformation("Searching for {name}, time elapsed (ms): {milliseconds}", name, _stopwatch.ElapsedMilliseconds);

			return Ok(result);
		}

		/// <summary>
		/// Get CityBikes networks
		/// </summary>
		/// <response code="200">Returns list of networks</response>
		[HttpGet("getCityBikesNetworksAsync")]
		public async Task<ActionResult<IEnumerable<NetworkDto>>> GetCityBikesNetworksAsync()
		{
			_stopwatch.Start();

			IList<Network> networks = await _repository.GetNetworksAsync().ConfigureAwait(false);
			List<NetworkDto> result = networks.ToDtoModel();

			_stopwatch.Stop();
			Logger.LogInformation("Loading CityBikes networks, time elapsed (ms): {milliseconds}", _stopwatch.ElapsedMilliseconds);

			return Ok(result);
		}
	}
}
