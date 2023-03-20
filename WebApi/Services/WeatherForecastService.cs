using WebApi.Repositories;

namespace WebApi.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository repo;

        public WeatherForecastService(IWeatherForecastRepository repo)
        {
            this.repo = repo;
        }
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => repo.Get(index))
            .ToArray();
        }
    }
}
