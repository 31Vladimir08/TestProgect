
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

namespace AspCoreTest.Services.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContactRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddContact(ContactDataModel contactDataModel)
        {
            throw new NotImplementedException();
        }

        public Task AddContactAsync(ContactDataModel contactDataModel)
        {
            throw new NotImplementedException();
        }

        public UserDataModel DeleteContact(int userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public UserDataModel GetUserContact(int userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataModel> GetUserContactAsync(int userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContactDataModel> GetUserContacts(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactDataModel>> GetUserContactsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public ContactDataModel SearchUserContacts(int userId, IQueryable<ContactDataModel> query)
        {
            throw new NotImplementedException();
        }

        public Task<ContactDataModel> SearchUserContactsAsync(int userId, IQueryable<ContactDataModel> query)
        {
            throw new NotImplementedException();
        }

        public void UpdateContact(ContactDataModel contactDataModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(ContactDataModel contactDataModel)
        {
            throw new NotImplementedException();
        }
    }
}
