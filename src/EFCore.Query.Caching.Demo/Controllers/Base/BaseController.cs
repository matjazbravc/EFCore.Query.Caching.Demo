using Microsoft.AspNetCore.Mvc;

namespace EFCoreQueryCachingDemo.Controllers.Base;

// Inject common services in a BaseController
public abstract class BaseController<T> : ControllerBase
  where T : BaseController<T>
{
  private ILogger<T>? _logger;

  protected ILogger<T>? Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
}
