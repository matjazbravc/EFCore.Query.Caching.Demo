namespace EFCoreQueryCachingDemo.Extensions
{
	/// <summary>
	/// WebApplicationBuilder extension to use old style Startup.cs
	/// Source: https://www.strathweb.com/2022/02/using-an-existing-startup-class-with-asp-net-6-minimal-hosting-model/
	/// </summary>
	public static class WebApplicationBuilderExtensions
	{
		public static WebApplication Build<TStartup>(this WebApplicationBuilder builder)
		{
			var startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration);
			if (startup == null)
			{
				throw new InvalidOperationException("Could not instantiate Startup!");
			}

			var configureServices = typeof(TStartup).GetMethod("ConfigureServices");
			if (configureServices == null)
			{
				throw new InvalidOperationException("Could not find ConfigureServices on Startup!");
			}

			configureServices.Invoke(startup, new object?[]
			{
				builder.Services
			});

			var app = builder.Build();

			var configure = typeof(TStartup).GetMethod("Configure");
			if (configure == null)
			{
				throw new InvalidOperationException("Could not find Configure on Startup!");
			}

			configure.Invoke(startup, new object?[]
			{
				app,
				app.Configuration,
				builder.Services
			});

			return app;
		}
	}
}