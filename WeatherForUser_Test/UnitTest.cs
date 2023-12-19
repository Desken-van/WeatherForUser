using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WeaterForUser_WebProfile.WebModels.Response;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Infrastructure.Repository;
using WeatherForUser_Infrastructure.Services;
using WeatherForUser_Infrastructure.Services.AppServices;
using WeatherForUser_Swagger.Controllers;
using WeatherForUser_Test.Settings;

namespace WeatherForUser_Test
{
    public class WeatherTests
    {
        private IMapper _mapper;

        private Mock<IUserRepository> _repo;

        private Mock<IUserService> _userService;
        private IWeatherService _weatherService;

        private WeatherForUserController _controller;

        [SetUp]
        public void Setup()
        {
            var dbContext = DBStore.GetDatabaseContext().Result;

            _mapper = MapperConfig.InitializeAutomapper();

            _repo = DBStore.CreateMockUserRepo();

            _userService = DBStore.CreateMockUserService();

            _weatherService = new WeatherService();

            _controller = new WeatherForUserController(_userService.Object, _weatherService);
        }

        [Test]
        public void GetUserTestSuccess()
        {
            // Arrange
            var expected = new User { Id = 2, Device = "DESKTOP-8DVNIFN", HashCode = "00ccae398ff14d5260b15d41b482d88b" };

            // Act
            var response = (_controller.GetUser().Result.Result as OkObjectResult);

            var result = response.Value as User;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.Device, result.Device);
            Assert.AreEqual(expected.HashCode, result.HashCode);
        }

        [Test]
        public void GetWeatherTestSuccess()
        {
            // Act
            var response = (_controller.GetWeather("Odessa").Result.Result as OkObjectResult);

            var result = response.Value as WeatherForecast;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, response.StatusCode);
        }
    }
}