using WeaterForUser_WebProfile.WebModels.Weather;

namespace WeaterForUser_WebProfile.WebModels.Response
{
    public class WeatherForecast
    {
        public WeatherResponse Weather { get; set; }

        public WeatherResponse HourWeather { get; set; }

        public WeatherResponseList WeekWeather { get; set; }
    }
}
