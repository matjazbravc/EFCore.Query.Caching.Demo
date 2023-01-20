using EFCoreQueryCachingDemo.Services.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EFCoreQueryCachingDemo.Services.Swagger
{
	/// <summary>
	/// Configures the Swagger UI options
	/// </summary>
	public class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
	{
		private readonly SwaggerConfig _swaggerConfig;
		private readonly IApiVersionDescriptionProvider _apiProvider;

		/// <summary>
		/// Initialises a new instance of the <see cref="ConfigureSwaggerUiOptions"/> class.
		/// </summary>
		/// <param name="apiProvider">The API provider.</param>
		/// <param name="swaggerConfig"></param>
		public ConfigureSwaggerUiOptions(IApiVersionDescriptionProvider apiProvider, IOptions<SwaggerConfig> swaggerConfig)
		{
			_apiProvider = apiProvider ?? throw new ArgumentNullException(nameof(apiProvider));
			_swaggerConfig = swaggerConfig.Value;
		}

		/// <inheritdoc />
		public void Configure(SwaggerUIOptions options)
		{
			options = options ?? throw new ArgumentNullException(nameof(options));
			options.RoutePrefix = _swaggerConfig.RoutePrefix;
			options.DocumentTitle = _swaggerConfig.Description;
			options.DocExpansion(DocExpansion.List);
			options.DefaultModelExpandDepth(0);

			// Configure Swagger JSON endpoints
			foreach (var description in _apiProvider.ApiVersionDescriptions)
			{
				var url = $"/{_swaggerConfig.RoutePrefix}/{description.GroupName}/{_swaggerConfig.DocsFile}";
				options.SwaggerEndpoint(url, description.GroupName);
			}
		}
	}
}
