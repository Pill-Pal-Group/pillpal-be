namespace PillPal.WebApi.Common;

[AttributeUsage(AttributeTargets.Method)]
public class CacheAttribute : Attribute, IAsyncActionFilter
{
    // cache key
    public string Key { get; set; } = default!;

    // cache object id
    public string IdParameterName { get; set; } = default!;

    // for get method, it will check if the cache is available, if not, it will call the method and cache the result
    // for post and put method, it will let request go through and then cache the result
    // for delete method, it will remove the cache
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cache = context.HttpContext.RequestServices.GetService<ICacheService>()
            ?? throw new InvalidOperationException("Cache service is not registered");

        // if the request is get, check if the cache is available
        if (context.HttpContext.Request.Method == "GET")
        {
            var cacheKey = $"{Key}:{context.ActionArguments[IdParameterName]}";

            var cacheData = await cache.GetAsync<object>(cacheKey);

            if (cacheData != null)
            {
                context.Result = new OkObjectResult(cacheData);
                return;
            }
        }

        var executedContext = await next();

        if (executedContext.Result is CreatedAtRouteResult createdResult)
        {
            var id = createdResult.RouteValues![IdParameterName];
            var cacheKey = $"{Key}:{id}";
            await cache.SetAsync(cacheKey, createdResult.Value);
        }
        else if (executedContext.Result is null)
        {
            // incase of known exception, the result will be null
            // for that case, ignore caching to prevent error
            return;
        }
        else
        {
            var cacheKey = $"{Key}:{context.ActionArguments[IdParameterName]}";

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cache.SetAsync(cacheKey, okObjectResult.Value);
            }

            if (executedContext.Result is NoContentResult)
            {
                await cache.RemoveAsync(cacheKey);
            }
        }
    }
}
