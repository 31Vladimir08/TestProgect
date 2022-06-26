using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;
using AspCoreTest.Services.Services;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Tests.Tests
{
    [TestClass]
    public class ContactRepositoryTests
    {
        private DbContextMemoryFactory _contextFactory;
        private IContactRepository _contactRepository;

        [TestInitialize]
        public void TestInit()
        {
            _contextFactory = new DbContextMemoryFactory();
            _contactRepository = new ContactRepository(_contextFactory);
            InitTestData();
        }

        [TestMethod]
        public void GetUserContactTest()
        {
            var actual = _contactRepository.GetUserContact(7, 2);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.AsNoTracking().First(x => x.UserId == 7 && x.ContactId == 2);

                var isTrue = () =>
                {
                    return actual != null
                        && actual.UserId == expected.UserId
                        && actual.ContactId == expected.ContactId
                        && actual.Id == expected.Id
                        && string.IsNullOrEmpty(actual.User.Passport);
                };
                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task GetUserContactAsyncTest()
        {
            var actual = await _contactRepository.GetUserContactAsync(7, 2);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.AsNoTracking().First(x => x.UserId == 7 && x.ContactId == 2);

                var isTrue = () =>
                {
                    return actual != null
                        && actual.UserId == expected.UserId
                        && actual.ContactId == expected.ContactId
                        && actual.Id == expected.Id
                        && string.IsNullOrEmpty(actual.User.Passport);
                };
                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void GetUserContactsTest()
        {
            var actual = _contactRepository.GetUserContacts(5);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.Where(x => x.UserId == 5).AsNoTracking().ToList();

                var isTrue = () =>
                {
                    return !actual.Any(x => !string.IsNullOrWhiteSpace(x.User.Passport) || x.UserId != 5) && actual.Count() == expected.Count();
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task GetUserContactsAsyncTest()
        {
            var actual = await _contactRepository.GetUserContactsAsync(5);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.Where(x => x.UserId == 5).AsNoTracking().ToList();

                var isTrue = () =>
                {
                    return !actual.Any(x => !string.IsNullOrWhiteSpace(x.User.Passport) || x.UserId != 5) && actual.Count() == expected.Count();
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void SearchUserContactsTest()
        {
            ContactDataModel? actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.Contact
                    .Join(
                        db.User.Where(x => x.Name == "TEST"),
                        contact => contact.ContactId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.ContactId,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = user
                        }
                    );
                actual = _contactRepository.SearchUserContacts(3, query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.First(x => x.UserId == 3 && x.ContactId == 8);

                var isTrue = () =>
                {
                    return actual != null 
                        && actual.Id == expected.Id
                        && actual.UserId == expected.UserId
                        && actual.ContactId == expected.ContactId
                        && string.IsNullOrEmpty(actual.User.Passport);
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task SearchUserContactsAsyncTest()
        {
            ContactDataModel? actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.Contact
                    .Join(
                        db.User.Where(x => x.Name == "TEST"),
                        contact => contact.ContactId,
                        user => user.Id,
                        (contact, user) => new ContactDataModel()
                        {
                            Id = contact.Id,
                            UserId = contact.UserId,
                            ContactId = contact.ContactId,
                            LastUpdateTime = contact.LastUpdateTime,
                            User = user
                        }
                    );
                actual = await _contactRepository.SearchUserContactsAsync(3, query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Contact.First(x => x.UserId == 3 && x.ContactId == 8);

                var isTrue = () =>
                {
                    return actual != null
                        && actual.Id == expected.Id
                        && actual.UserId == expected.UserId
                        && actual.ContactId == expected.ContactId
                        && string.IsNullOrEmpty(actual.User.Passport);
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void AddContactTest()
        {
            var newContact = new ContactDataModel() { UserId = 1, ContactId = 8 } ;
            _contactRepository.AddContact(newContact);

            using (var db = _contextFactory.CreateDbContext())
            {
                var t = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == 1 && x.ContactId == 8);

                Assert.IsNotNull(t);
            }
        }

        [TestMethod]
        public async Task AddContactAsyncTest()
        {
            var newContact = new ContactDataModel() { UserId = 1, ContactId = 7 };
            await _contactRepository.AddContactAsync(newContact);

            using (var db = _contextFactory.CreateDbContext())
            {
                var t = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == 1 && x.ContactId == 7);

                Assert.IsNotNull(t);
            }
        }

        [TestMethod]
        public void UpdateContactTest()
        {
            ContactDataModel contactDataModel;
            using (var db = _contextFactory.CreateDbContext())
            {
                contactDataModel = db.Contact.AsNoTracking().First(x => x.Id == 1);
                contactDataModel.ContactId = 6;
            }

            _contactRepository.UpdateContact(contactDataModel);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.Contact.AsNoTracking().First(x => x.Id == 1);

                var isTrue = () =>
                {
                    return actual.ContactId == 6;
                };
                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task UpdateContactAsyncTest()
        {
            ContactDataModel contactDataModel;
            using (var db = _contextFactory.CreateDbContext())
            {
                contactDataModel = db.Contact.AsNoTracking().First(x => x.Id == 1);
                contactDataModel.ContactId = 6;
            }

            await _contactRepository.UpdateContactAsync(contactDataModel);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.Contact.AsNoTracking().First(x => x.Id == 1);

                var isTrue = () =>
                {
                    return actual.ContactId == 6;
                };
                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void DeleteContactTest()
        {
            _contactRepository.DeleteContact(1, 5);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == 1 && x.ContactId == 5);

                var isTrue = () =>
                {
                    return actual == null;
                };
                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task DeleteContactAsyncTest()
        {
            await _contactRepository.DeleteContactAsync(1, 3);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.Contact.AsNoTracking().FirstOrDefault(x => x.UserId == 1 && x.ContactId == 3);

                var isTrue = () =>
                {
                    return actual == null;
                };
                Assert.IsTrue(isTrue());
            }
        }

        private void InitTestData()
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                try
                {
                    db.User.AddRange
                            (
                                new List<UserDataModel>()
                                {
                                    new UserDataModel() { Id = 1, Name = "UserTest1", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 2, Name = "UserTest2", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 3, Name = "UserTest3", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 4, Name = "UserTest4", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 5, Name = "TEST", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 6, Name = "TEST", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 7, Name = "TEST", Passport = "54321", State = true },
                                    new UserDataModel() { Id = 8, Name = "TEST", Passport = "54321", State = true }
                                }
                            );

                    db.Contact.AddRange
                        (
                            new List<ContactDataModel>()
                            {
                                new ContactDataModel() { Id = 1, UserId = 1, ContactId = 2},
                                new ContactDataModel() { Id = 2, UserId = 1, ContactId = 3},
                                new ContactDataModel() { Id = 3, UserId = 1, ContactId = 4},
                                new ContactDataModel() { Id = 4, UserId = 1, ContactId = 5},
                                new ContactDataModel() { Id = 5, UserId = 2, ContactId = 1},
                                new ContactDataModel() { Id = 6, UserId = 2, ContactId = 6},
                                new ContactDataModel() { Id = 7, UserId = 2, ContactId = 7},
                                new ContactDataModel() { Id = 8, UserId = 2, ContactId = 8},
                                new ContactDataModel() { Id = 9, UserId = 3, ContactId = 2},
                                new ContactDataModel() { Id = 10, UserId = 3, ContactId = 8},
                                new ContactDataModel() { Id = 11, UserId = 4, ContactId = 1},
                                new ContactDataModel() { Id = 12, UserId = 4, ContactId = 3},
                                new ContactDataModel() { Id = 13, UserId = 4, ContactId = 7},
                                new ContactDataModel() { Id = 14, UserId = 5, ContactId = 8},
                                new ContactDataModel() { Id = 15, UserId = 5, ContactId = 3},
                                new ContactDataModel() { Id = 16, UserId = 5, ContactId = 6},
                                new ContactDataModel() { Id = 17, UserId = 6, ContactId = 8},
                                new ContactDataModel() { Id = 18, UserId = 7, ContactId = 1},
                                new ContactDataModel() { Id = 19, UserId = 7, ContactId = 2},
                                new ContactDataModel() { Id = 20, UserId = 7, ContactId = 3}
                            }
                        );

                    db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}