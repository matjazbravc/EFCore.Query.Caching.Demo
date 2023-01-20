using EFCoreQueryCachingDemo.Models.Dto;
using EFCoreQueryCachingDemo.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace EFCoreQueryCachingDemo.HttpClients
{
	public class CityBikesHttpClient : ICityBikesHttpClient
	{
		private readonly Uri _baseUri = new("https://api.citybik.es/v2/");
		private readonly ILogger<CityBikesHttpClient> _logger;
		private readonly IHttpClientFactory _clientFactory;

		public CityBikesHttpClient(ILogger<CityBikesHttpClient> logger, IHttpClientFactory clientFactory)
		{
			_logger = logger;
			_clientFactory = clientFactory;
		}

		public async Task<CityBikesNetworks?> GetCityBikesNetworksAsync(CancellationToken cancellationToken)
		{
			return await SendGetRequest<CityBikesNetworks>("networks", cancellationToken).ConfigureAwait(false);
		}

		private static TData? Convert<TData>(string content)
		{
			var jsonSettings = new JsonSerializerSettings
			{
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
			};

			var dateTimeConverter = new IsoDateTimeConverter
			{
				DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
			};
			jsonSettings.Converters.Add(dateTimeConverter);

			return JsonConvert.DeserializeObject<TData>(content, jsonSettings);
		}

		private async Task<TData?> SendGetRequest<TData>(string relativePath, CancellationToken cancellationToken)
		{
			TData? result = default;
			var httpClient = _clientFactory.CreateClient("PollyHttpClient");
			var uri = new UriBuilder(new Uri(_baseUri, relativePath));
			HttpResponseMessage apiResult = await httpClient.GetAsync(uri.Uri, cancellationToken).ConfigureAwait(false);
			if (apiResult.IsSuccessStatusCode)
			{
				var strContent = await apiResult.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
				result = cancellationToken.IsCancellationRequested ?
					default :
					Convert<TData>(strContent);
			}
			else
			{
				_logger.LogError("WARNING: Call to {cityBikesUri} has failed! Error code: {apiResult}", uri, apiResult.ToString());
			}
			return result;
		}
	}
}
