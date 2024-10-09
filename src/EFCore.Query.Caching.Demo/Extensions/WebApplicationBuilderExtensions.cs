namespace EFCoreQueryCachingDemo.Extensions;

/// <summary>
/// WebApplicationBuilder extension to use old style Startup.cs
/// Source: https://www.strathweb.com/2022/02/using-an-existing-startup-class-with-asp-net-6-minimal-hosting-model/
/// </summary>
public static class WebApplicationBuilderExtensions
{
  public static WebApplication Build<TStartup>(this WebApplicationBuilder builder)
  {
    object startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration)
      ?? throw new InvalidOperationException("Could not instantiate Startup!");

    System.Reflection.MethodInfo configureServices = typeof(TStartup).GetMethod("ConfigureServices")
      ?? throw new InvalidOperationException("Could not find ConfigureServices on Startup!");

    configureServices.Invoke(startup, new object?[]
    {
        builder.Services
    });

    WebApplication app = builder.Build();

    System.Reflection.MethodInfo configure = typeof(TStartup).GetMethod("Configure")
      ?? throw new InvalidOperationException("Could not find Configure on Startup!");

    configure.Invoke(startup,
    [
      app,
      app.Configuration,
      builder.Services
  ]);

    return app;
  }
}