namespace coordinator;

public class UnhandledExceptionMiddleware
{
    private readonly ILogger<UnhandledExceptionMiddleware> logger;
    private readonly RequestDelegate next;

    public UnhandledExceptionMiddleware(RequestDelegate next, ILogger<UnhandledExceptionMiddleware> logger)
    {
        this.logger = logger;
        this.next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        try
        {
            return next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Uncaught exception in {context.Request.Path}: {e.Message}");
        }

        return Task.CompletedTask;
    }
}
