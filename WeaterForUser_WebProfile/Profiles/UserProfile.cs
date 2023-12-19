using AutoMapper;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Core.Entities;

namespace WeaterForUser_WebProfile.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<UserSettingsEntity, UserSettings>().ReverseMap();
        }
    }
}
