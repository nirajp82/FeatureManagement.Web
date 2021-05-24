using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagement.Web.Infrastructure
{
    public class TimeElapsedFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                ActionExecutionDelegate next)
        {
            Stopwatch timer = Stopwatch.StartNew();
            await Task.Delay(1);
            await next();
            timer.Stop();
            string message = " Elapsed time: " + $"{timer.Elapsed.TotalMilliseconds} ms";
            context.HttpContext.Response.Headers.Add("TotalTimeTaken", message);
        }
    }
}
