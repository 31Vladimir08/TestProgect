
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

namespace AspCoreTest.Services.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddUser(UserDataModel userDataModel)
        {
            throw new NotImplementedException();
        }

        public Task AddUserAsync(UserDataModel userDataModel)
        {
            throw new NotImplementedException();
        }

        public UserDataModel GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataModel> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDataModel GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataModel> GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public UserDataModel SearchUserByName(IQueryable<UserDataModel> query)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataModel> SearchUserByNameAsync(IQueryable<UserDataModel> query)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserDataModel userDataModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UserDataModel userDataModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserState(int userId, bool userState)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserStateAsync(int userId, bool userState)
        {
            throw new NotImplementedException();
        }
    }
}
