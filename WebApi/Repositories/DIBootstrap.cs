namespace WebApi.Repositories
{
    public static class DIBootstrap
    {
        public static IServiceCollection RegisterWeatherForecastRepo(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
            return services;
        }
    }
}
