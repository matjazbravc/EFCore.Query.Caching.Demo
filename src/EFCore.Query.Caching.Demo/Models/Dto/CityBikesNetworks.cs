using Newtonsoft.Json;

namespace EFCoreQueryCachingDemo.Models.Dto;

public class CityBikesNetworks
{
  [JsonProperty("networks", NullValueHandling = NullValueHandling.Ignore)]
  public List<NetworkDto> Networks { get; set; }
}
