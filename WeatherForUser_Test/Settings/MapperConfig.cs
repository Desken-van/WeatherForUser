using AutoMapper;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Core.Entities;

namespace WeatherForUser_Test.Settings
{
    public static class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, User>().ReverseMap();
                cfg.CreateMap<UserSettingsEntity, UserSettings>().ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
