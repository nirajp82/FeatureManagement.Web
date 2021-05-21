using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagement.Web.Infrastructure
{
    public class CustomDisabledFeatureHandler : IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features,
                                            ActionExecutingContext context)
        {
            string message = $"The following features are not available at this time {string.Join(',', features)}";
            context.Result = new ContentResult()
            {
                ContentType = "text/plain",
                Content = message,
                StatusCode = 404
            };
            return Task.CompletedTask;
        }
    }
}
