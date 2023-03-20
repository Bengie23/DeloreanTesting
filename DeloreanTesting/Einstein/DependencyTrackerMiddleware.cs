using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Work in progress
    /// </summary>
    public class DependencyTrackerMiddleware : IMiddleware
    {
        private readonly IDependencyTracker DependencyTracker;

        public DependencyTrackerMiddleware(IDependencyTracker dependencyTracker)
        {
            this.DependencyTracker = dependencyTracker;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
            var test = DependencyTracker.GetTrackingData();
            await next(context);
        }
    }

    public static class DependencyTrackerMiddlewareExtensions
    {
        public static IApplicationBuilder UseDependencyTrackerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DependencyTrackerMiddleware>();
        }
    }
}
