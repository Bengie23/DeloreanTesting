namespace WebApi.Repositories
{
    public interface IWeatherForecastRepository
    {
        WeatherForecast Get(int index);
    }
}
