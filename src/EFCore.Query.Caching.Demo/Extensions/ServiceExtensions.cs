using Asp.Versioning.Conventions;
using Asp.Versioning;
using EFCoreQueryCachingDemo.Services.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EFCoreQueryCachingDemo.Extensions;

/// <summary>
/// Service extensions
/// </summary>
public static class ServiceExtensions
{
  /// <summary>
  /// Adds service API versioning
  /// </summary>
  /// <param name="services"></param>
  public static void AddAndConfigureApiVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(options =>
    {
      // Specify the default API Version
      options.DefaultApiVersion = new ApiVersion(1, 0);
      // Read the API version specified by client
      options.ApiVersionReader = new UrlSegmentApiVersionReader();
      // Use default version when version is not specified
      options.AssumeDefaultVersionWhenUnspecified = true;
      // Advertise the API versions supported for the particular endpoint
      options.ReportApiVersions = true;
    })
    // Adds a convention to let Swagger understand the different API versions
    .AddMvc(options => options.Conventions.Add(new VersionByNamespaceConvention()))
    .AddApiExplorer(options =>
    {
      // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
      // Note: the specified format code will format the version as "'v'major[.minor][-status]"
      options.GroupNameFormat = "'v'VVV";
      // Note: this option is only necessary when versioning by url segment.
      // The SubstitutionFormat can also be used to control the format of the API version in route templates.
      options.SubstituteApiVersionInUrl = true;
    });
  }

  /// <summary>
  /// Adds cross-origin resource sharing services
  /// </summary>
  /// <param name="services"></param>
  /// <param name="policyName"></param>
  public static void AddCorsPolicy(this IServiceCollection services, string policyName)
  {
    services.AddCors(options =>
    {
      options.AddPolicy(policyName,
        builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .WithExposedHeaders("X-Pagination"));
    });
  }

  /// <summary>
  /// Adds Swagger support
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public static void AddSwaggerMiddleware(this IServiceCollection services)
  {
    // Configure Swagger Options
    services.AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUiOptions>();
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

    // Register the Swagger generator
    services.AddSwaggerGen();
  }
}