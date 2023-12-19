using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.Response;
using WeaterForUser_WebProfile.WebModels.User;

namespace WeatherForUser_Infrastructure.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetWeatherAsync(UserSettings settings);
        Task<WeatherForecast> GetWeatherAsync(string city);
        WeatherAnnounce CheckWeather(WeatherForecast weather);
    }
}
