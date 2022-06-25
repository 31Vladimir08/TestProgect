using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AspCoreTest.Services.Models
{
    public class MessageDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Content { get; set; }
        public UserDataModel User { get; set; }
    }

    public class MessageDataModelConfig : IEntityTypeConfiguration<MessageDataModel>
    {
        public void Configure(EntityTypeBuilder<MessageDataModel> builder)
        {
            builder.ToTable("Messages");
            builder.HasOne(p => p.User).WithMany(t => t.Message).HasForeignKey(p => p.UserId);
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.ContactId);
        }
    }
}
