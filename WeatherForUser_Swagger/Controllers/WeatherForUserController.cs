using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.Response;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Infrastructure.Services;

namespace WeatherForUser_Swagger.Controllers
{
    public class WeatherForUserController : Controller
    {
        private IUserService _userService;
        private IWeatherService _weatherService;

        public WeatherForUserController(IUserService userService, IWeatherService weatherService)
        {
            _userService = userService;
            _weatherService = weatherService;
        }

        [HttpGet("api/user/get")]
        public async Task<ActionResult<User>> GetUser()
        {
            try
            {
                var user = await _userService.CheckingUserAsync();

                if (user != null)
                {
                    return Ok(user);
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/weather/get")]
        public async Task<ActionResult<WeatherForecast>> GetWeather(string city)
        {
            try
            {
                var user = await _userService.CheckingUserAsync();
                var weather = new WeatherForecast();

                if (user != null)
                {
                    var setting = await _userService.CheckingUserSettingsAsync(user, city);

                    if (setting != null)
                    {
                        weather = await _weatherService.GetWeatherAsync(setting);

                        if (weather != null)
                        {
                            return Ok(weather);
                        }
                    }
                }

                weather = await _weatherService.GetWeatherAsync(city);

                if (weather != null)
                {
                    return Ok(weather);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/weather/anounce")]
        public async Task<ActionResult<WeatherForecast>> CheckRain(string? city)
        {
            try
            {
                var user = await _userService.CheckingUserAsync();

                var weather = new WeatherForecast();
                
                var announce = new WeatherAnnounce();

                if (user != null)
                {
                    var setting = await _userService.GetUserSettingsAsync(user);

                    if (setting != null)
                    {
                        weather = await _weatherService.GetWeatherAsync(setting);

                        announce = _weatherService.CheckWeather(weather);

                        if (announce != null)
                        {
                            return Ok(announce);
                        }
                    }
                }

                weather = await _weatherService.GetWeatherAsync(city);

                announce = _weatherService.CheckWeather(weather);

                if (announce != null)
                {
                    return Ok(announce);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
