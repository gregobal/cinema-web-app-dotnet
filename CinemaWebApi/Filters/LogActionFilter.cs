using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaWebApi.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogDebug("On action executing");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogDebug("On action executed");
    }
}