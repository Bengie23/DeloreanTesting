namespace WebApi.Services
{
    public static class DIBootstrap
    {
        public static IServiceCollection RegisterWeatherForecastService(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherForecastService,WeatherForecastService>();
            return services;
        }
    }
}
