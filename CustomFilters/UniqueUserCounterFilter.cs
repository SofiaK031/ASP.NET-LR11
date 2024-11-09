// дані виводяться в кожному новому рядку
using Microsoft.AspNetCore.Mvc.Filters;

namespace LR11.CustomFilters
{
    public class UniqueUserCounterFilter : IActionFilter
    {
        private static readonly HashSet<string> uniqueSessions = new HashSet<string>();
        private static readonly object lockObject = new();  // Для блокування доступу
        private readonly string logFilePath;

        public UniqueUserCounterFilter(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            uniqueSessions.Add(sessionIp);

            using (var streamWriter = new StreamWriter(logFilePath, true))
            {
                lock (lockObject)
                {
                    uniqueSessions.Add(sessionIp);
                }

                lock (lockObject)
                {
                    streamWriter.WriteLine($"Number of unique users (by sessions): {uniqueSessions.Count}, " +
                        $"Date and time: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}





// змінюється число в одному першому полі
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace LR11.CustomFilters
//{
//    public class UniqueUserCounterFilter : IAsyncActionFilter
//    {
//        private static readonly HashSet<string> uniqueSessions = new HashSet<string>();
//        private static readonly object lockObject = new();  // Для блокування доступу
//        private readonly string logFilePath;

//        public UniqueUserCounterFilter(string logFilePath)
//        {
//            this.logFilePath = logFilePath;
//        }

//        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {
//            var sessionId = context.HttpContext.Session.Id;

//            lock (lockObject)
//            {
//                uniqueSessions.Add(sessionId);
//            }

//            lock (lockObject)
//            {
//                File.WriteAllText(logFilePath, $"Number of unique users (by sessions): {uniqueSessions.Count}, " +
//                    $"Date and time: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
//            }

//            await next();
//        }
//    }
//}