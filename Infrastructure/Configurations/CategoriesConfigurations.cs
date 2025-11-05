using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CategoriesConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(c => c.Description).IsRequired().HasMaxLength(300).IsUnicode();
            builder.Property(c => c.EmojiIcon).IsRequired().HasMaxLength(10);
            builder.Property(c => c.BackgroundColorHEX).IsRequired().HasMaxLength(7);
        }
    }
}
