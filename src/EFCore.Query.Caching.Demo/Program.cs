using EFCoreQueryCachingDemo;
using EFCoreQueryCachingDemo.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Create the logger and setup sinks, filters and properties
var logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host
	// Set the content root to be the current directory
	.UseContentRoot(Directory.GetCurrentDirectory())
	.ConfigureAppConfiguration((builderContext, config) =>
	{
		var env = builderContext.HostingEnvironment;
		config.SetBasePath(env.ContentRootPath);
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
		config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
		config.AddEnvironmentVariables();
	});

var app = builder.Build<Startup>();
app.Run();