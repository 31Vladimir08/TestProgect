using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Models
{

    public class UserDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        public bool State { get; set; }
        public ICollection<MessageDataModel> Message { get; set; }
        public ICollection<ContactDataModel> Contact { get; set; }
    }

    public class UserDataModelConfig : IEntityTypeConfiguration<UserDataModel>
    {
        public void Configure(EntityTypeBuilder<UserDataModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
        }
    }
}
