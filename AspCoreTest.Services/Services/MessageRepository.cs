
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

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
            throw new NotImplementedException();
        }

        public Task AddMessageAsync(MessageDataModel messageDataModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MessageDataModel> GetUserMessages(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDataModel>> GetUserMessagesAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public MessageDataModel SearchUserMessages(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDataModel> SearchUserMessagesAsync(int userId, int contactId, IQueryable<MessageDataModel> query)
        {
            throw new NotImplementedException();
        }
    }
}
