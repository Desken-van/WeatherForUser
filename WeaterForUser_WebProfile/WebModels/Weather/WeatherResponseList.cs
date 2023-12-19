using System.Collections.Generic;

namespace WeaterForUser_WebProfile.WebModels.Weather
{
    public class WeatherResponseList
    {
        public City City { get; set; }
        public List<WeatherList> List { get; set; }
    }
}
