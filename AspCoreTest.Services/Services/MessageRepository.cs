
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public MessageRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddMessage(MessageDataModel messageDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Message.Add(messageDataModel);
                context.SaveChanges();
            }
        }

        public async Task AddMessageAsync(MessageDataModel messageDataModel)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Message.Add(messageDataModel);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<MessageDataModel> GetUserMessages(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Message.Where(x => x.UserId == userId).AsNoTracking().ToList();
            }
        }

        public async Task<IEnumerable<MessageDataModel>> GetUserMessagesAsync(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Message.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
            }
        }

        public IEnumerable<MessageDataModel?> SearchUserMessages(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            return query.Where(x => x.UserId == userId && x.ContactId == contactId).AsNoTracking().ToList();
        }

        public async Task<IEnumerable<MessageDataModel?>> SearchUserMessagesAsync(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            return await query.Where(x => x.UserId == userId && x.ContactId == contactId).AsNoTracking().ToListAsync();
        }
    }
}
