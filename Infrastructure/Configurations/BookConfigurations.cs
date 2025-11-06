using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Category).WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(b => !b.SoftDelete);

            builder.Property(b => b.Title).IsRequired().HasMaxLength(200).IsUnicode();
            builder.Property(b => b.Description).IsRequired().HasMaxLength(500).IsUnicode();
            builder.Property(b => b.Author).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(b => b.PagesNumber).IsRequired().IsUnicode();
            builder.Property(b => b.Price).IsRequired().IsUnicode();
            builder.Property(b => b.ImageUrl).IsRequired();
            builder.Property(b => b.SoftDelete).HasDefaultValue(false);
            builder.Property(b => b.SoftDelete).HasDefaultValue(false);
        }
    }
}
