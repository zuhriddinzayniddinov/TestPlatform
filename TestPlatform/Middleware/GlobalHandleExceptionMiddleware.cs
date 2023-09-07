using System.Diagnostics;

namespace TestPlatform.API.Middleware;

public class GlobalHandleExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalHandleExceptionMiddleware> _logger;
    private readonly Stopwatch _watch;
    public GlobalHandleExceptionMiddleware(RequestDelegate next,
        ILogger<GlobalHandleExceptionMiddleware> logger,
        Stopwatch watch)
    {
        _next = next;
        _logger = logger;
        _watch = watch;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _watch.Restart();
            await _next(context);
            _watch.Stop();
            _logger.Log(LogLevel.Information, message: _watch.ElapsedMilliseconds.ToString(), null!);
        }
        catch (Exception e)
        {
            _watch.Stop();
            _logger.Log(LogLevel.Error, message: e.Message + " : " + _watch.ElapsedMilliseconds.ToString(), e);
        }
    }
}