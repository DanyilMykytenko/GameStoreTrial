namespace WebApi.Middleware;

public class CustomErrorLoggingMiddleware
{
    private readonly ILogger<CustomErrorLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomErrorLoggingMiddleware(ILogger<CustomErrorLoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occured while processing the request {context.Request.Path} - {context.Request.Method} in {ex.TargetSite.DeclaringType.FullName}");

            throw;
        }
    }
}