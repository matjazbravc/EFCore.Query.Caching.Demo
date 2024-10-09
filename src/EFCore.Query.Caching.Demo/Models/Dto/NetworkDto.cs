using EFCoreQueryCachingDemo.Services.Converters;
using Newtonsoft.Json;

namespace EFCoreQueryCachingDemo.Models.Dto;

public class NetworkDto
{
  [JsonConverter(typeof(StringConverter))]
  [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
  public string Company { get; set; }

  [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
  public string Href { get; set; }

  [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
  public string Id { get; set; }

  [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
  public LocationDto Location { get; set; }

  [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
  public string Name { get; set; }

  [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
  public string Source { get; set; }
}
