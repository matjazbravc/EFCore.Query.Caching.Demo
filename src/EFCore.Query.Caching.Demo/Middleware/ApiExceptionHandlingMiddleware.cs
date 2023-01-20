using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreQueryCachingDemo.Middleware
{
	/// <summary>
	/// Global Exception Handling Middleware
	/// </summary>
	public class ApiExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

		public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong: {ex}", ex);
				await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
			}
		}

		/// <summary>
		/// Handle exception with modifying response
		/// </summary>
		/// <param name="httpContext">HttpContext</param>
		/// <param name="ex">Exception</param>
		/// <returns>Task</returns>
		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var errorMsg = ex.Message;
			if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
			{
				errorMsg = ex.InnerException.Message;
			}

			_logger.LogError("{errorMsg}, REQ: {path}", errorMsg, httpContext.Request.Path);

			var problemDetails = new ProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
				Title = "Internal Server Error",
				Status = httpContext.Response.StatusCode,
				Instance = httpContext.Request.Path,
				Detail = errorMsg
			};

			var result = JsonSerializer.Serialize(problemDetails);
			await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
		}
	}
}