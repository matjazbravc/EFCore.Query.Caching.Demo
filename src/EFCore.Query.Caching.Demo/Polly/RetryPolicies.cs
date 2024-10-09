using Polly;
using Polly.Extensions.Http;

namespace EFCoreQueryCachingDemo.Polly;

public static class RetryPolicies
{
  public static IAsyncPolicy<HttpResponseMessage> GetHttpClientRetryPolicy()
  {
    return HttpPolicyExtensions
      // Handle HttpRequestExceptions, 408 and 5xx status codes
      .HandleTransientHttpError()
      // Handle 404 not found
      .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
      // Handle 401 Unauthorized
      .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
      // What to do if any of the above erros occur:
      // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
      .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
  }
}
