using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;
using AspCoreTest.Services.Services;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Tests.Tests
{
    [TestClass]
    public class MessageRepositoryTests
    {
        private DbContextMemoryFactory _contextFactory;
        private IMessageRepository _messageRepository;

        [TestInitialize]
        public void TestInit()
        {
            _contextFactory = new DbContextMemoryFactory();
            _messageRepository = new MessageRepository(_contextFactory);
            InitTestData();
        }

        [TestMethod]
        public void GetUserMessagesTest()
        {
            var actual = _messageRepository.GetUserMessages(1);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => x.UserId == 1).ToList();

                var isTrue = () =>
                {
                    return actual != null
                        && !actual.Any(x => x.UserId != 1)
                        && actual.Count() == expected.Count();
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task GetUserMessagesAsyncTest()
        {
            var actual = await _messageRepository.GetUserMessagesAsync(1);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => x.UserId == 1).ToList();

                var isTrue = () =>
                {
                    return actual != null
                        && !actual.Any(x => x.UserId != 1)
                        && actual.Count() == expected.Count();
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void SearchUserMessagesTest()
        {
            IEnumerable<MessageDataModel> actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.Message.Where(x => EF.Functions.Like(x.Content, "%was%"));
                actual = _messageRepository.SearchUserMessages(1, 2, query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => EF.Functions.Like(x.Content, "%was%") && x.UserId == 1 && x.ContactId == 2).ToList();

                var isTrue = () =>
                {
                    return actual != null
                    && actual.Count() == expected.Count()
                    && !actual.Any(x => x.UserId != 1 || x.ContactId != 2);
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task SearchUserMessagesAsyncTest()
        {
            IEnumerable<MessageDataModel> actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.Message.Where(x => EF.Functions.Like(x.Content, "%was%"));
                actual = await _messageRepository.SearchUserMessagesAsync(1, 2, query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => EF.Functions.Like(x.Content, "%was%") && x.UserId == 1 && x.ContactId == 2).ToList();

                var isTrue = () =>
                {
                    return actual != null
                    && actual.Count() == expected.Count()
                    && !actual.Any(x => x.UserId != 1 || x.ContactId != 2);
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void AddMessageTest()
        {
            var newMessage = new MessageDataModel()
            {
                UserId = 8,
                ContactId = 1,
                Content = "TestAddMessage"
            };

            _messageRepository.AddMessage(newMessage);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => x.Content == "TestAddMessage").ToList();
                var first = expected.First();

                var isTrue = () =>
                {
                    return expected.Count() == 1
                        && first.Content == "TestAddMessage"
                        && first.UserId == 8
                        && first.ContactId == 1;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task AddMessageAsyncTest()
        {
            var newMessage = new MessageDataModel()
            {
                UserId = 1,
                ContactId = 8,
                Content = "TestAddMessageAsync"
            };

            await _messageRepository.AddMessageAsync(newMessage);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.Message.Where(x => x.Content == "TestAddMessageAsync").ToList();
                var first = expected.First();

                var isTrue = () =>
                {
                    return expected.Count() == 1
                    && first.Content == "TestAddMessageAsync"
                    && first.UserId == 1
                    && first.ContactId == 8;
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

                    db.Message.AddRange
                            (
                                new List<MessageDataModel>()
                                {
                                    new MessageDataModel() { Id = 1, UserId = 1, ContactId = 2, Content = "Hi. How are you?" },
                                    new MessageDataModel() { Id = 2, UserId = 1, ContactId = 2, Content = "I'm fine." },
                                    new MessageDataModel() { Id = 3, UserId = 1, ContactId = 2, Content = "I was very tired yesterday." },
                                    new MessageDataModel() { Id = 4, UserId = 1, ContactId = 2, Content = "I was sick" },
                                    new MessageDataModel() { Id = 5, UserId = 1, ContactId = 3, Content = "Test" },
                                    new MessageDataModel() { Id = 6, UserId = 1, ContactId = 4, Content = "TEst" },
                                    new MessageDataModel() { Id = 7, UserId = 2, ContactId = 1, Content = "I'm fine. Thanks." },
                                    new MessageDataModel() { Id = 8, UserId = 2, ContactId = 1, Content = "And you?" },
                                    new MessageDataModel() { Id = 9, UserId = 2, ContactId = 1, Content = "Why?" },
                                    new MessageDataModel() { Id = 10, UserId = 2, ContactId = 1, Content = "I'm watching TV now" },
                                    new MessageDataModel() { Id = 11, UserId = 2, ContactId = 1, Content = "What are you doing?" },
                                    new MessageDataModel() { Id = 12, UserId = 2, ContactId = 1, Content = "12345" },
                                    new MessageDataModel() { Id = 13, UserId = 2, ContactId = 1, Content = "12345" },
                                    new MessageDataModel() { Id = 14, UserId = 2, ContactId = 3, Content = "12345" },
                                    new MessageDataModel() { Id = 15, UserId = 2, ContactId = 4, Content = "12345" },
                                    new MessageDataModel() { Id = 16, UserId = 3, ContactId = 1, Content = "12345" },
                                    new MessageDataModel() { Id = 17, UserId = 3, ContactId = 2, Content = "12345" },
                                    new MessageDataModel() { Id = 18, UserId = 3, ContactId = 4, Content = "12345" },
                                    new MessageDataModel() { Id = 19, UserId = 4, ContactId = 1, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 20, UserId = 4, ContactId = 2, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 21, UserId = 4, ContactId = 3, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 22, UserId = 5, ContactId = 6, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 23, UserId = 5, ContactId = 7, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 24, UserId = 5, ContactId = 8, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 25, UserId = 6, ContactId = 5, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 26, UserId = 6, ContactId = 7, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 27, UserId = 6, ContactId = 8, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 28, UserId = 7, ContactId = 5, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 29, UserId = 7, ContactId = 6, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 30, UserId = 7, ContactId = 8, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 31, UserId = 8, ContactId = 5, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 32, UserId = 8, ContactId = 6, Content = "sdhgdsjhsdj" },
                                    new MessageDataModel() { Id = 33, UserId = 8, ContactId = 7, Content = "sdhgdsjhsdj" }
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