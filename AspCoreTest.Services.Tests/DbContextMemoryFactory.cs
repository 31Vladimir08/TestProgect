
using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Tests
{
    internal class DbContextMemoryFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            return new AppDbContext(options);
        }
    }
}
