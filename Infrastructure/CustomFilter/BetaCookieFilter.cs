using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Threading.Tasks;

namespace FeatureManagement.Web.Infrastructure
{
    [FilterAlias("Custom.CookieFilter")]
    public class BetaCookieFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BetaCookieFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            //bool isEnabled = _httpContextAccessor.HttpContext
            //                                     .Request
            //                                     .Cookies.ContainsKey("beta");

            CookiePresentFilterSettings settings = context.Parameters.Get<CookiePresentFilterSettings>();
            bool isEnabled = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(settings.CookieName);

            return Task.FromResult(isEnabled);
        }
    }
}
