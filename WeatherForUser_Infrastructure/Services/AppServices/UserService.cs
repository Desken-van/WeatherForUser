using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Infrastructure.Repository;

namespace WeatherForUser_Infrastructure.Services.AppServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CheckingUserAsync()
        {
            var device = System.Environment.MachineName;

            var hash = GetHashString(device);

            if (hash != null)
            {
                var user = await _userRepository.GetUserAsync(device, hash);

                if (user == null)
                {
                    var userResponse = new User()
                    {
                        Device = device,
                        HashCode = hash
                    };

                    await _userRepository.CreateUserAsync(userResponse);

                    user = await _userRepository.GetUserAsync(device, hash);
                }

                return user;
            }

            return null;
        }

        public async Task<UserSettings> CheckingUserSettingsAsync(User user, string city)
        {
            var setting = await _userRepository.GetUserSettingsByUserIdAsync(user.Id);

            if (setting != null)
            {
                setting.LastUsedCity = city;

                await _userRepository.UpdateUserSettingsAsync(setting);

                return setting;
            }

            var newSetting = new UserSettings()
            {
                UserId = user.Id,
                LastUsedCity = city
            };

            await _userRepository.CreateUserSettingsAsync(newSetting);

            return newSetting;
        }

        public async Task<UserSettings> GetUserSettingsAsync(User user)
        {
            var setting = await _userRepository.GetUserSettingsByUserIdAsync(user.Id);

            if (setting != null)
            {
                return setting;
            }

            return null;
        }

        private string GetHashString(string device)
        {
            var bytes = Encoding.Unicode.GetBytes(device);

            var CSP = new MD5CryptoServiceProvider();

            var byteHash = CSP.ComputeHash(bytes);

            var hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
