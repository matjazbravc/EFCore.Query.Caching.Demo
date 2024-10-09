using EFCoreQueryCachingDemo.Models.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EFCoreQueryCachingDemo.HttpClients;

public class CityBikesHttpClient(
  ILogger<CityBikesHttpClient> logger,
  IHttpClientFactory clientFactory)
: ICityBikesHttpClient
{
  private readonly Uri _baseUri = new("https://api.citybik.es/v2/");
  private readonly ILogger<CityBikesHttpClient> _logger = logger;

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
    var httpClient = clientFactory.CreateClient("PollyHttpClient");
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
      _logger.LogError("WARNING: Call to {CityBikesUri} has failed! Error code: {ApiResult}", uri, apiResult.ToString());
    }
    return result;
  }
}
