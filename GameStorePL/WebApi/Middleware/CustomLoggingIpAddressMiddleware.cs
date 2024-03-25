namespace WebApi.Middleware;

public class CustomLoggingIpAddressMiddleware
{
    private readonly ILogger<CustomLoggingIpAddressMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomLoggingIpAddressMiddleware(ILogger<CustomLoggingIpAddressMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"GameStore request: {context.Request.Method} from {context.Connection.RemoteIpAddress}");
        await _next(context);
    }
}