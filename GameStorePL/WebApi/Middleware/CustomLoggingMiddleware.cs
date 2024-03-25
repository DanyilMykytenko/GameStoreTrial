using System.Diagnostics;

namespace WebApi.Middleware;

public class CustomLoggingMiddleware
{
    private readonly ILogger<CustomLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomLoggingMiddleware(ILogger<CustomLoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _next(context);
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        _logger.LogInformation($"GameStore responce: {context.Response.StatusCode} and took: {elapsedMilliseconds}");
    }
}