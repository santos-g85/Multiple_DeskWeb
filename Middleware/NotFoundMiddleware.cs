using Microsoft.AspNetCore.Mvc;
public class NotFoundMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<NotFoundMiddleware> _logger;
    public NotFoundMiddleware(RequestDelegate next,
        ILogger<NotFoundMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Response.Body;

        using (var memstream = new MemoryStream())
        {
            context.Response.Body = memstream;

            await _next(context);

            if(context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogWarning("404 Not Found: {Path}", context.Request.Path);
                context.Response.Body = originalBody;

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                var result = new ViewResult
                {
                    ViewName = "NotFound"
                };
                var actioncontext = new ActionContext(
                    context,
                    new RouteData(),
                    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
                    );
                await result.ExecuteResultAsync(actioncontext);

                return;
            }
            memstream.Seek(0, SeekOrigin.Begin);
            await memstream.CopyToAsync(originalBody);
        }
    }
}

    
