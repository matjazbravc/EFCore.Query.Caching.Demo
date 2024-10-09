using EFCoreQueryCachingDemo.Services.Configuration;
using StackExchange.Redis;

namespace EFCoreQueryCachingDemo.Services.Helpers;

public static class RedisHelper
{
  public static ConnectionMultiplexer ConnectRedis(EasyCachingConfig config)
  {
    ConfigurationOptions redisOptions = ConfigurationOptions.Parse($"{config.DbConfig_Endpoint}:{config.DbConfig_Port}");
    redisOptions.AbortOnConnectFail = false;
    redisOptions.AllowAdmin = config.DbConfig_AllowAdmin;
    redisOptions.Password = config.DbConfig_Password;
    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(redisOptions);
    return multiplexer;
  }
}
