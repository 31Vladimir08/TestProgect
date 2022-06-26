
using AspCoreTest.Services.Models;

namespace AspCoreTest.Services.Interfaces
{
    public interface IContactRepository
    {
        ContactDataModel? GetUserContact(int userId, int contactId);
        Task<ContactDataModel?> GetUserContactAsync(int userId, int contactId);
        IEnumerable<ContactDataModel> GetUserContacts(int userId);
        Task<IEnumerable<ContactDataModel>> GetUserContactsAsync(int userId);
        ContactDataModel? SearchUserContacts(int userId, IQueryable<ContactDataModel> query);
        Task<ContactDataModel?> SearchUserContactsAsync(int userId, IQueryable<ContactDataModel> query);
        void AddContact(ContactDataModel contactDataModel);
        Task AddContactAsync(ContactDataModel contactDataModel);
        void UpdateContact(ContactDataModel contactDataModel);
        Task UpdateContactAsync(ContactDataModel contactDataModel);
        void DeleteContact(int userId, int contactId);
        Task DeleteContactAsync(int userId, int contactId);
    }
}
