using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.Api.Data
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<Models.TaskItem>
    {
        public void Configure(EntityTypeBuilder<Models.TaskItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(500);
            builder.Property(t => t.Completed).IsRequired();
            builder.Property(t => t.CreationDate).IsRequired();
        }
    }
}
