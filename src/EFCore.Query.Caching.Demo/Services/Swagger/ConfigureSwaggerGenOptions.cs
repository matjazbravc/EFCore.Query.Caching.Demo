using Asp.Versioning.ApiExplorer;
using EFCoreQueryCachingDemo.Services.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace EFCoreQueryCachingDemo.Services.Swagger;

/// <summary>
/// Configures the Swagger generation options
/// </summary>
/// <remarks>This allows API versioning to define a Swagger document per API version after the
/// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
  private readonly IApiVersionDescriptionProvider _apiProvider;
  private readonly SwaggerConfig _swaggerConfig;

  /// <summary>
  /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class
  /// </summary>
  /// <param name="apiProvider">The <see cref="IApiVersionDescriptionProvider">apiProvider</see> used to generate Swagger documents.</param>
  /// <param name="swaggerConfig"></param>
  public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiProvider, IOptions<SwaggerConfig> swaggerConfig)
  {
    _apiProvider = apiProvider ?? throw new ArgumentNullException(nameof(apiProvider));
    _swaggerConfig = swaggerConfig.Value;
  }

  /// <inheritdoc />
  public void Configure(SwaggerGenOptions options)
  {
    // Add a swagger document for each discovered API version
    // Note: you might choose to skip or document deprecated API versions differently
    foreach (var description in _apiProvider.ApiVersionDescriptions)
    {
      options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

      // Include Document file
      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      options.IncludeXmlComments(xmlPath);

      // Provide a custom strategy for generating the unique Id's
      options.CustomSchemaIds(x => x.FullName);
    }
  }

  /// <summary>
  /// Create API version
  /// </summary>
  /// <param name="description"></param>
  /// <returns></returns>
  private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
  {
    var info = new OpenApiInfo()
    {
      Title = _swaggerConfig.Title,
      Version = description.ApiVersion.ToString(),
      Description = _swaggerConfig.Description,
      Contact = new OpenApiContact
      {
        Name = _swaggerConfig.ContactName,
        Email = _swaggerConfig.ContactEmail,
        Url = new Uri(_swaggerConfig.ContactUrl)
      },
      License = new OpenApiLicense
      {
        Name = _swaggerConfig.LicenseName,
        Url = new Uri(_swaggerConfig.LicenseUrl)
      }
    };

    if (description.IsDeprecated)
    {
      info.Description += " ** THIS API VERSION HAS BEEN DEPRECATED!";
    }

    return info;
  }
}