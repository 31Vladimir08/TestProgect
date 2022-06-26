using AspCoreTest.Services.Interfaces;
using AspCoreTest.Services.Models;
using AspCoreTest.Services.Services;

namespace AspCoreTest.Services.Tests.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private DbContextMemoryFactory _contextFactory;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void TestInit()
        {
            _contextFactory = new DbContextMemoryFactory();
            _userRepository = new UserRepository(_contextFactory);
            InitTestData();
        }

        [TestMethod]
        public void AddUserTest()
        {
            var newUser = new UserDataModel() { Name = "User1", Passport = "12345", State = false };
            _userRepository.AddUser(newUser);

            using (var db = _contextFactory.CreateDbContext())
            {
                var t = db.User.FirstOrDefault(x => x.Name == "User1");

                Assert.IsNotNull(t);
            }
        }

        [TestMethod]
        public async Task AddUserAsyncTest()
        {
            var newUser = new UserDataModel() { Name = "User2", Passport = "54321", State = true };
            
            await _userRepository.AddUserAsync(newUser);

            using (var db = _contextFactory.CreateDbContext())
            {
                var t = db.User.FirstOrDefault(x => x.Name == "User2");

                Assert.IsNotNull(t);
            }
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var actual = _userRepository.GetUserById(3);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.First(x => x.Id == 3);

                var isTrue = () =>
                {
                    return actual != null
                        && actual.Id == expected.Id;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task GetUserByIdAsyncTest()
        {
            var actual = await _userRepository.GetUserByIdAsync(3);

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.First(x => x.Id == 3);

                var isTrue = () =>
                {
                    return actual != null
                        && actual.Id == expected.Id;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var actual = _userRepository.GetUserByName("UserTest4");

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.First(x => x.Name == "UserTest4");

                var isTrue = () =>
                {
                    return actual != null
                        && actual.Id == expected.Id
                        && actual.Name == expected.Name;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task GetUserByNameAsyncTest()
        {
            var actual = await _userRepository.GetUserByNameAsync("UserTest4");

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.First(x => x.Name == "UserTest4");

                var isTrue = () =>
                {
                    return actual != null
                        && actual.Id == expected.Id
                        && actual.Name == expected.Name;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            UserDataModel userDataModel;
            using (var db = _contextFactory.CreateDbContext())
            {
                userDataModel = db.User.First(x => x.Id == 1);
                userDataModel.Passport = "00000";
            }
            _userRepository.UpdateUser(userDataModel);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.User.FirstOrDefault(x => x.Id == 1);

                var isTrue = () =>
                {
                    return actual != null && actual.Passport == "00000";
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task UpdateUserAsyncTest()
        {
            UserDataModel userDataModel;
            using (var db = _contextFactory.CreateDbContext())
            {
                userDataModel = db.User.First(x => x.Id == 2);
                userDataModel.Passport = "11111";
            }
            await _userRepository.UpdateUserAsync(userDataModel);            

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.User.FirstOrDefault(x => x.Id == 2);

                var isTrue = () =>
                {
                    return actual != null && actual.Passport == "11111";
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void UpdateUserStateTest()
        {
            _userRepository.UpdateUserState(1, false);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.User.FirstOrDefault(x => x.Id == 1);

                var isTrue = () =>
                {
                    return actual != null && actual.State == false;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task UpdateUserStateAsyncTest()
        {
            await _userRepository.UpdateUserStateAsync(2, false);

            using (var db = _contextFactory.CreateDbContext())
            {
                var actual = db.User.FirstOrDefault(x => x.Id == 2);

                var isTrue = () =>
                {
                    return actual != null && actual.State == false;
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public void SearchUserByNameTest()
        {
            IEnumerable<UserDataModel> actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.User.Where(x => x.Name == "TEST");
                actual = _userRepository.SearchUserByName(query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.Where(x => x.Name == "TEST").ToList();

                var isTrue = () =>
                {
                    return actual != null && actual.Count() == expected.Count();
                };

                Assert.IsTrue(isTrue());
            }
        }

        [TestMethod]
        public async Task SearchUserByNameAsyncTest()
        {
            IEnumerable<UserDataModel> actual;
            using (var db = _contextFactory.CreateDbContext())
            {
                var query = db.User.Where(x => x.Name == "TEST");
                actual = await _userRepository.SearchUserByNameAsync(query);
            }

            using (var db = _contextFactory.CreateDbContext())
            {
                var expected = db.User.Where(x => x.Name == "TEST").ToList();
                var isTrue = () =>
                {
                    return actual != null && actual.Count() == expected.Count();
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
                                    new UserDataModel() { Id = 7, Name = "TEST", Passport = "54321", State = true }
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