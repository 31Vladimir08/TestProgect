using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Models
{
    public class ContactDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public UserDataModel User { get; set; }
        public UserDataModel UserContact { get; set; }
    }

    public class ContactDataModelConfig : IEntityTypeConfiguration<ContactDataModel>
    {
        public void Configure(EntityTypeBuilder<ContactDataModel> builder)
        {
            builder.ToTable("Contacts");
            builder.HasOne(p => p.User).WithMany(t => t.Contact).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.UserContact).WithMany(t => t.Contact).HasForeignKey(p => p.ContactId);
            builder.HasKey(u => u.Id);
        }
    }
}
