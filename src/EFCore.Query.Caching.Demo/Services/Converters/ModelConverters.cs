using EFCoreQueryCachingDemo.Models;
using EFCoreQueryCachingDemo.Models.Dto;

namespace EFCoreQueryCachingDemo.Services.Converters;

public static class ModelConverters
{
  public static List<Network> ToEfCoreModel(this List<NetworkDto> networks)
  {
    return (from networkDto in networks
            let location = new Location
            {
              City = networkDto.Location.City,
              Country = networkDto.Location.Country,
              Latitude = networkDto.Location.Latitude,
              Longitude = networkDto.Location.Longitude
            }
            select new Network
            {
              Id = networkDto.Id,
              Name = networkDto.Name,
              Company = networkDto.Company,
              Href = networkDto.Href,
              Source = networkDto.Source,
              Location = location
            }).ToList();
  }

  public static List<NetworkDto> ToDtoModel(this IList<Network> networks)
  {
    return (from network in networks
            let location = new LocationDto
            {
              City = network.Location.City,
              Country = network.Location.Country,
              Latitude = network.Location.Latitude,
              Longitude = network.Location.Longitude
            }
            select new NetworkDto
            {
              Id = network.Id,
              Name = network.Name,
              Company = network.Company,
              Href = network.Href,
              Source = network.Source,
              Location = location
            }).ToList();
  }
}