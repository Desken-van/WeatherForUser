using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.Response;
using WeaterForUser_WebProfile.WebModels.User;
using WeaterForUser_WebProfile.WebSettings;
using WeatherForUser_Infrastructure.Services.Helpers;

namespace WeatherForUser_Infrastructure.Services.AppServices
{
    public class WeatherService : IWeatherService
    {
        private readonly List<int> weatherCodes = new List<int>() {803, 804, 500, 501, 502, 503, 504, 511, 520, 521, 522, 531};

        public async Task<WeatherForecast> GetWeatherAsync(UserSettings settings) => await GetWeatherAsync(settings.LastUsedCity);

        public async Task<WeatherForecast> GetWeatherAsync(string city)
        {
            var url = $"{WebSettings.ApiUrl}?q={city}&appid={WebSettings.ApiKey}&units=metric";
            var hourUrl = $"{WebSettings.HourApiUrl}?q={city}&appid={WebSettings.ApiKey}&units=metric";

            try
            {
                var weather = await WeatherHelper.GetWeather(url);

                var futureWeather = await WeatherHelper.GetWeatherHour(hourUrl);

                var weekWeather = await WeatherHelper.GetWeatherWeek(hourUrl);

                var result = new WeatherForecast()
                {
                    Weather = weather,
                    HourWeather = futureWeather,
                    WeekWeather = weekWeather
                };

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public WeatherAnnounce CheckWeather(WeatherForecast weather)
        {
            var result = new WeatherAnnounce();

            if (weatherCodes.Contains(weather.Weather.Weather[0].Id))
            {
                result.CurrentWeatherCheck = true;
            }
            if (weatherCodes.Contains(weather.HourWeather.Weather[0].Id))
            {
                result.WeatherInHourCheck = true;
            }

            var index = 0;

            var weekWeather = new WeatherAnnounceWeek();

            foreach (var entity in weather.WeekWeather.List)
            {
                if (weatherCodes.Contains(entity.Weather[0].Id))
                {
                    weekWeather.WeekWeatherCheck = true;

                    weekWeather.WeatherIndex = index;

                    result.WeekWeather = weekWeather;

                    break;
                }

                index++;
            }

            return result;

        }
    }

}
