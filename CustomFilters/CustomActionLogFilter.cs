using Microsoft.AspNetCore.Mvc.Filters;

namespace LR11.CustomFilters
{
    public class CustomActionLogFilter : Attribute, IAsyncActionFilter
    {
        private readonly string _pathToLogFile;
        private readonly object _lock = new object();

        public CustomActionLogFilter(string pathToLogFile)
        {
            _pathToLogFile = pathToLogFile;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            var message = $"Controller: {controllerName}, Action (tab): {actionName}, " +
                          $"Date and time: {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n";

            lock (_lock)
            {
                File.AppendAllText(_pathToLogFile, message);
            }

            return next();
        }
    }
}

