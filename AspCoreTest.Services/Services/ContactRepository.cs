
using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public ContactRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddContact(ContactDataModel contactDataModel)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                db.Contact.Add(contactDataModel);
                db.SaveChanges();
            }
        }

        public async Task AddContactAsync(ContactDataModel contactDataModel)
        {
            using (var db = await _contextFactory.CreateDbContextAsync())
            {
                db.Contact.Add(contactDataModel);
                await db.SaveChangesAsync();
            }
        }

        public void DeleteContact(int userId, int contactId)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var t = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == userId && x.ContactId == contactId);
                if (t == null)
                    return;
                db.Contact.Remove(t);
                db.SaveChanges();
            }
        }

        public async Task DeleteContactAsync(int userId, int contactId)
        {
            using (var db = await _contextFactory.CreateDbContextAsync())
            {
                var t = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == userId && x.ContactId == contactId);
                if (t == null)
                    return;
                db.Contact.Remove(t);
                await db.SaveChangesAsync();
            }
        }

        public ContactDataModel? GetUserContact(int userId, int contactId)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                return db.Contact.Where(x => x.UserId == userId && x.ContactId == contactId)
                    .Join(
                        db.User,
                        contact => contact.UserId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.ContactId,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = new UserDataModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                State = user.State
                            }
                        }
                    ).AsNoTracking().FirstOrDefault();
            }
        }

        public async Task<ContactDataModel?> GetUserContactAsync(int userId, int contactId)
        {
            using (var db = await _contextFactory.CreateDbContextAsync())
            {
                return await db.Contact.Where(x => x.UserId == userId && x.ContactId == contactId)
                    .Join(
                        db.User,
                        contact => contact.UserId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.ContactId,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = new UserDataModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                State = user.State
                            }
                        }
                    ).AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public IEnumerable<ContactDataModel> GetUserContacts(int userId)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                return db.Contact.Where(x => x.UserId == userId)
                    .Join(
                        db.User,
                        contact => contact.UserId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.Id,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = new UserDataModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                State = user.State
                            }
                        }
                    ).AsNoTracking().ToList();
            }
        }

        public async Task<IEnumerable<ContactDataModel>> GetUserContactsAsync(int userId)
        {
            using (var db = await _contextFactory.CreateDbContextAsync())
            {
                return await db.Contact.Where(x => x.UserId == userId)
                    .Join(
                        db.User,
                        contact => contact.UserId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.Id,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = new UserDataModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                State = user.State
                            }
                        }
                    ).AsNoTracking().ToListAsync();
            }
        }

        public ContactDataModel? SearchUserContacts(int userId, IQueryable<ContactDataModel> query)
        {
            return query.Select(x => new ContactDataModel() 
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ContactId = x.ContactId,
                    LastUpdateTime = x.LastUpdateTime,
                    User = new UserDataModel()
                    {
                        Id = x.User.Id,
                        Name = x.User.Name,
                        State = x.User.State
                    }
                }).FirstOrDefault(x => x.UserId == userId);
        }

        public async Task<ContactDataModel?> SearchUserContactsAsync(int userId, IQueryable<ContactDataModel> query)
        {
            return await query.Select(x => new ContactDataModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                ContactId = x.ContactId,
                LastUpdateTime = x.LastUpdateTime,
                User = new UserDataModel()
                {
                    Id = x.User.Id,
                    Name = x.User.Name,
                    State = x.User.State
                }
            }).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public void UpdateContact(ContactDataModel contactDataModel)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                db.Contact.Update(contactDataModel);
                db.SaveChanges();
            }
        }

        public async Task UpdateContactAsync(ContactDataModel contactDataModel)
        {
            using (var db = await _contextFactory.CreateDbContextAsync())
            {
                db.Contact.Update(contactDataModel);
                await db.SaveChangesAsync();
            }
        }
    }
}
