using System.Collections.Generic;

namespace WeaterForUser_WebProfile.WebModels.Weather
{
    public class WeatherList
    {
        public List<Weather> Weather { get; set; }
        public TemparatureInfo Main { get; set; } 
        public string Dt_txt { get; set; }
    }
}
