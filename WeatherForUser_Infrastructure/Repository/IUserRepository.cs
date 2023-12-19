using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.User;

namespace WeatherForUser_Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string device, string hashcode);
        Task<User> GetUserByIdAsync(int Id);
        Task<UserSettings> GetUserSettingsByIdAsync(int id);
        Task<UserSettings> GetUserSettingsByUserIdAsync(int id);
        Task CreateUserAsync(User user);
        Task CreateUserSettingsAsync(UserSettings settings);
        Task UpdateUserSettingsAsync(UserSettings settings);
    }
}
