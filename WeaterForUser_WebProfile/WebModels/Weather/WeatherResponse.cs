using System.Collections.Generic;

namespace WeaterForUser_WebProfile.WebModels.Weather
{
    public class WeatherResponse
    {
        public List<Weather> Weather { get; set; }
        public string Name { get; set; }
        public TemparatureInfo Main { get; set; }
    }
}
