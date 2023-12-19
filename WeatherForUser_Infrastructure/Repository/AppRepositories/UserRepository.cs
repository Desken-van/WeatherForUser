using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherForUser_Core;
using WeatherForUser_Core.Entities;
using WeaterForUser_WebProfile.WebModels.User;

namespace WeatherForUser_Infrastructure.Repository.AppRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WFU_Context _dbcontext;
        private readonly IMapper _mapper;

        public UserRepository(WFU_Context dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(string device, string hashcode)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Device == device && x.HashCode == hashcode);

            if (user != null)
            {
                var result = _mapper.Map<User>(user);

                return result;
            }

            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Id == id);


            if (user != null)
            {
                var result = _mapper.Map<User>(user);

                return result;
            }

            return null;
        }

        public async Task<UserSettings> GetUserSettingsByIdAsync(int id)
        {
            var settings = await _dbcontext.UserSettings.FirstOrDefaultAsync(x => x.Id == id);


            if (settings != null)
            {
                var result = _mapper.Map<UserSettings>(settings);

                return result;
            }

            return null;
        }

        public async Task<UserSettings> GetUserSettingsByUserIdAsync(int id)
        {
            var settings = await _dbcontext.UserSettings.FirstOrDefaultAsync(x => x.UserId == id);

            if (settings != null)
            {
                var result = _mapper.Map<UserSettings>(settings);

                return result;
            }

            return null;
        }

        public async Task CreateUserAsync(User user)
        {
            var result =  _mapper.Map<UserEntity>(user);

            await _dbcontext.Users.AddAsync(result);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task CreateUserSettingsAsync(UserSettings settings)
        {
            var result = _mapper.Map<UserSettingsEntity>(settings);

            await _dbcontext.UserSettings.AddAsync(result);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateUserSettingsAsync(UserSettings settings)
        {
            var original = await _dbcontext.UserSettings.FindAsync(settings.Id);
            _dbcontext.Entry(original).CurrentValues.SetValues(settings);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
