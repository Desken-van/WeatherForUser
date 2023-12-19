using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeaterForUser_WebProfile.WebModels.Weather;

namespace WeatherForUser_Infrastructure.Services.Helpers
{
    public class WeatherHelper
    {
        private static async Task<string> GetResponse(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }

            return response;
        }
        public static async Task<WeatherResponse> GetWeather(string url)
        {
            var response = await GetResponse(url);
            var weather = JsonConvert.DeserializeObject<WeatherResponse>(response);

            return weather;
        }
        public static async Task<WeatherResponse> GetWeatherHour(string url)
        {
            var response = await GetResponse(url);
            var weather = JsonConvert.DeserializeObject<WeatherResponseList>(response);
            var weatherResponse = GetHourWeather(weather);     

            return weatherResponse;
        }

        public static async Task<WeatherResponseList> GetWeatherWeek(string url)
        {
            var response = await GetResponse(url);
            var weather = JsonConvert.DeserializeObject<WeatherResponseList>(response);
            var weatherResponse = GetWeekWeather(weather);

            return weatherResponse;
        }

        private static DateTime GetDatedate(string datestr)
        {
            datestr = datestr.Replace("-", " ");
            datestr = datestr.Replace(":", " ");
            var list = new List<int>(datestr.Split(' ').Select(int.Parse));

            var date = new DateTime(list[0], list[1], list[2], list[3], list[4], list[5]);
            return date;
        }
        private static WeatherResponse GetHourWeather(WeatherResponseList weather)
        {
            var now = DateTime.Now;
            var timelist = new List<DateTime>();

            foreach (var entity in weather.List)
            {
                var time = GetDatedate(entity.Dt_txt);

                if ((time.Year == now.Year) && (time.Month == now.Month) && (time.Day == now.Day))
                {
                    timelist.Add(time);
                }
            }

            var cur = new DateTime();

            if (timelist.Count == 1)
            {
                cur = timelist[0];
            }
            else
            {
                cur = timelist[1];
            }

            var weatherResponse = new WeatherResponse();

            foreach (var entity in weather.List)
            {
                var time = GetDatedate(entity.Dt_txt);

                if (time == cur)
                {
                    weatherResponse.Name = weather.City.Name;
                    weatherResponse.Main = entity.Main;
                    weatherResponse.Weather = entity.Weather;
                }
            }

            return weatherResponse;       
        }

        private static WeatherResponseList GetWeekWeather(WeatherResponseList weather)
        {
            var currentdate = new DateType();
            var newdate = new DateType();
            var currenttime = GetDatedate(weather.List[0].Dt_txt);

            currentdate.Year = currenttime.Year;
            currentdate.Month = currenttime.Month;
            currentdate.Day = currenttime.Day;

            var sum = weather.List[0].Main.Temp;
            var count = 1;

            var total = new List<WeatherList>();

            var index = 0;

            foreach (var entity in weather.List)
            {
                var newtime = GetDatedate(entity.Dt_txt);

                newdate.Year = newtime.Year;
                newdate.Month = newtime.Month;
                newdate.Day = newtime.Day;

                if (currentdate.Year == newdate.Year && currentdate.Day == newdate.Day && currentdate.Month == newdate.Month)
                {
                    sum += entity.Main.Temp;
                    count += 1;
                }
                else if (index == weather.List.Count - 1)
                {
                    var temp = new TemparatureInfo() { Temp = sum / count };

                    var weath = new WeatherList() { Main = temp, Dt_txt = $"{currentdate.Year}.{currentdate.Month}.{currentdate.Day}", Weather = entity.Weather};

                    total.Add(weath);
                }
                else
                {
                    var temp = new TemparatureInfo() { Temp = sum / count };
                    var weath = new WeatherList() { Main = temp, Dt_txt = $"{currentdate.Year}.{currentdate.Month}.{currentdate.Day}", Weather = entity.Weather };

                    total.Add(weath);

                    currentdate.Year = newdate.Year;
                    currentdate.Month = newdate.Month;
                    currentdate.Day = newdate.Day;

                    sum = entity.Main.Temp;
                    count = 1;
                }

                index++;
            }

            var weekResponse = new WeatherResponseList()
            {
                City = weather.City,
                List = total,
            };

            return weekResponse;
        }
    }
}
