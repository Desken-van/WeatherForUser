namespace WeaterForUser_WebProfile.WebModels.Response
{
    public class WeatherAnnounce
    {
        public bool CurrentWeatherCheck { get; set; }

        public bool WeatherInHourCheck { get; set; }

        public WeatherAnnounceWeek WeekWeather { get; set; }


    }

    public class WeatherAnnounceWeek
    {
        public int WeatherIndex { get; set; }

        public bool WeekWeatherCheck { get; set; }
    }
}
