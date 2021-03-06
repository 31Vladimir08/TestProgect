
using AspCoreTest.Services.Models;

namespace AspCoreTest.Services.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<MessageDataModel> GetUserMessages(int userId);
        Task<IEnumerable<MessageDataModel>> GetUserMessagesAsync(int userId);
        IEnumerable<MessageDataModel> SearchUserMessages(int userId, int contactId, IQueryable<MessageDataModel> query);
        Task<IEnumerable<MessageDataModel>> SearchUserMessagesAsync(int userId, int contactId, IQueryable<MessageDataModel> query);
        void AddMessage(MessageDataModel messageDataModel);
        Task AddMessageAsync(MessageDataModel messageDataModel);
    }
}
