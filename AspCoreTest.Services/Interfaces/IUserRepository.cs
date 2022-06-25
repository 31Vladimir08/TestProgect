
using AspCoreTest.Services.Models;

namespace AspCoreTest.Services.Interfaces
{
    public interface IUserRepository
    {
        UserDataModel? GetUserById(int userId);
        Task<UserDataModel?> GetUserByIdAsync(int userId);
        UserDataModel? GetUserByName(string username);
        Task<UserDataModel?> GetUserByNameAsync(string username);
        IEnumerable<UserDataModel?> SearchUserByName(IQueryable<UserDataModel> query);
        Task<IEnumerable<UserDataModel?>> SearchUserByNameAsync(IQueryable<UserDataModel> query);
        void AddUser(UserDataModel userDataModel);
        Task AddUserAsync(UserDataModel userDataModel);
        void UpdateUser(UserDataModel userDataModel);
        Task UpdateUserAsync(UserDataModel userDataModel);
        void UpdateUserState(int userId, bool userState);
        Task UpdateUserStateAsync(int userId, bool userState);
    }
}
