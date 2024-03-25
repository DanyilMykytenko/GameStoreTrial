using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace WebApi.Attributes;

public class LoggingAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Log.Information($"[{context.Controller}]: on the {context.HttpContext.Request.Path} - {context.HttpContext.Request.Method} " +
            $"with data: {context.HttpContext.Request.Headers.Values.ToList()}, model state: {context.ModelState}");

        base.OnActionExecuting(context);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Log.Information($"[{context.Controller}] on the {context.HttpContext.Request.Path} - {context.HttpContext.Request.Method}" +
            $"Responce code: {context.HttpContext.Response.StatusCode} with final data: {context.HttpContext.Response.Headers.Values.ToList()}");

        base.OnResultExecuted(context);
    }
}
