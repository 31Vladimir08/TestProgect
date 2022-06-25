
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public UserRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddUser(UserDataModel UserDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.User.Add(UserDataModel);
                context.SaveChanges();
            }
        }

        public async Task AddUserAsync(UserDataModel userDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.User.Add(userDataModel);
                await context.SaveChangesAsync();
            }
        }

        public UserDataModel? GetUserById(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.User.Where(x => x.Id == userId).AsNoTracking().FirstOrDefault();
            }
        }

        public async Task<UserDataModel?> GetUserByIdAsync(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.User.Where(x => x.Id == userId).AsNoTracking().FirstOrDefaultAsync();
            }                
        }

        public UserDataModel? GetUserByName(string username)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.User.Where(x => x.Name == username).AsNoTracking().FirstOrDefault();
            }                
        }

        public async Task<UserDataModel?> GetUserByNameAsync(string username)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.User.Where(x => x.Name == username).AsNoTracking().FirstOrDefaultAsync();
            }                
        }

        public IEnumerable<UserDataModel?> SearchUserByName(IQueryable<UserDataModel> query)
        {
            return query.ToList();
        }

        public async Task<IEnumerable<UserDataModel?>> SearchUserByNameAsync(IQueryable<UserDataModel> query)
        {
            return await query.ToListAsync();
        }

        public void UpdateUser(UserDataModel userDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.User.Update(userDataModel);
                context.SaveChanges();
            }                
        }

        public async Task UpdateUserAsync(UserDataModel userDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.User.Update(userDataModel);
                await context.SaveChangesAsync();
            }                
        }

        public void UpdateUserState(int userId, bool userState)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var t = context.User.Where(x => x.Id == userId).FirstOrDefault();
                if (t == null)
                    return;
                t.State = userState;
                context.SaveChanges();
            }
        }

        public async Task UpdateUserStateAsync(int userId, bool userState)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var t = context.User.Where(x => x.Id == userId).FirstOrDefault();
                if (t == null)
                    return;
                t.State = userState;
                await context.SaveChangesAsync();
            }
        }
    }
}
