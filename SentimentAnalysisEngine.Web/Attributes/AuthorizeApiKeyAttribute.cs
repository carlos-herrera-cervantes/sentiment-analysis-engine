using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SentimentAnalysisEngine.Web.Attributes
{
    public class AuthorizeApiKeyAttribute : TypeFilterAttribute
    {
        public AuthorizeApiKeyAttribute() : base(typeof(AuthorizeApiKeyFilter)) { }

        private class AuthorizeApiKeyFilter : IAsyncActionFilter
        {
            private readonly ILogger<AuthorizeApiKeyFilter> _logger;

            public AuthorizeApiKeyFilter(ILogger<AuthorizeApiKeyFilter> logger)
                => _logger = logger;

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var apiKey = context.HttpContext.Request.Headers["x-api-key"].ToString();
                _logger.LogInformation($"API KEY IS: {apiKey}");

                await next();
            }
        }
    }
}
