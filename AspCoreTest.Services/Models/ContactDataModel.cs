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
    }

    public class ContactDataModelConfig : IEntityTypeConfiguration<ContactDataModel>
    {
        public void Configure(EntityTypeBuilder<ContactDataModel> builder)
        {
            builder.ToTable("Contacts");
            builder.HasOne(p => p.User).WithMany(t => t.Contact).HasForeignKey(p => p.UserId);
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.ContactId);
        }
    }
}
