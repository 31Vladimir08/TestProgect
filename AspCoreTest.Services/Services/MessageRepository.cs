
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;
        public MessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddMessage(MessageDataModel messageDataModel)
        {
            _appDbContext.Set<MessageDataModel>().Add(messageDataModel);
            _appDbContext.SaveChanges();
        }

        public async Task AddMessageAsync(MessageDataModel messageDataModel)
        {
            _appDbContext.Set<MessageDataModel>().Add(messageDataModel);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<MessageDataModel> GetUserMessages(int userId)
        {
            return _appDbContext.Set<MessageDataModel>().Where(x => x.UserId == userId).AsNoTracking().ToList();
        }

        public async Task<IEnumerable<MessageDataModel>> GetUserMessagesAsync(int userId)
        {
            return await _appDbContext.Set<MessageDataModel>().Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public MessageDataModel? SearchUserMessages(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            return query.Where(x => x.UserId == userId && x.ContactId == contactId).AsNoTracking().FirstOrDefault();
        }

        public async Task<MessageDataModel?> SearchUserMessagesAsync(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            return await query.Where(x => x.UserId == userId && x.ContactId == contactId).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
