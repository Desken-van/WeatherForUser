using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.User;

namespace WeatherForUser_Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> CheckingUserAsync();
        Task<UserSettings> CheckingUserSettingsAsync(User user, string city);
        Task<UserSettings> GetUserSettingsAsync(User user);
    }
}
