using AspCoreTest.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDataModel> User { get; set; }
        public DbSet<ContactDataModel> Contact { get; set; }
        public DbSet<MessageDataModel> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDataModelConfig());
            modelBuilder.ApplyConfiguration(new ContactDataModelConfig());
            modelBuilder.ApplyConfiguration(new MessageDataModelConfig());
        }
    }
}