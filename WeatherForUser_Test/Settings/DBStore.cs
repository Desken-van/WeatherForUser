using Microsoft.EntityFrameworkCore;
using Moq;
using WeaterForUser_WebProfile.WebModels.User;
using WeatherForUser_Core;
using WeatherForUser_Core.Entities;
using WeatherForUser_Infrastructure.Repository;
using WeatherForUser_Infrastructure.Services;

public static class DBStore
{
    public static Mock<IUserRepository> CreateMockUserRepo()
    {
        var repo = new Mock<IUserRepository>();

        var res = new User { Id = 2, Device = "DESKTOP-8DVNIFN", HashCode = "00ccae398ff14d5260b15d41b482d88b" };

        repo.Setup(_user_repo => _user_repo.GetUserAsync("DESKTOP-8DVNIFN", "00ccae398ff14d5260b15d41b482d88b").Result)
            .Returns(res);

        return repo;
    }

    public static Mock<IUserService> CreateMockUserService()
    {
        var serv = new Mock<IUserService>();

        var res = new User { Id = 2, Device = "DESKTOP-8DVNIFN", HashCode = "00ccae398ff14d5260b15d41b482d88b" };

        serv.Setup(_user_serv => _user_serv.CheckingUserAsync().Result)
            .Returns(res);

        return serv;
    }

    public static async Task<WFU_Context> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<WFU_Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new WFU_Context(options);
        databaseContext.Database.EnsureCreated();

        var userList = GetUserEntityList();

        foreach (var user in userList)
        {
            databaseContext.Users.Add(user);
        }

        var settingsList = GetUserSettingsEntityList();

        foreach (var settings in settingsList)
        {
            databaseContext.UserSettings.Add(settings);
        }

        await databaseContext.SaveChangesAsync();

        return databaseContext;
    }

    public static List<UserEntity> GetUserEntityList()
    {
        var result = new List<UserEntity>
            {
                new UserEntity { Id = 1, Device = "Test", HashCode = "Test" },
                new UserEntity { Id = 2, Device = "DESKTOP-8DVNIFN", HashCode = "00ccae398ff14d5260b15d41b482d88b" },
            };

        return result;

    }
    public static List<UserSettingsEntity> GetUserSettingsEntityList()
    {
        var result = new List<UserSettingsEntity>
            {
                new UserSettingsEntity { Id = 1, UserId = 2, LastUsedCity = "Odessa"},
            };

        return result;
    }
}