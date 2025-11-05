using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(255);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(60);
            builder.Property(u => u.IsActive).HasDefaultValue(false);
            builder.Property(u => u.Role).HasDefaultValue(UserRoleEnum.User);
            builder.Property(u => u.ProfileImageUrl).HasDefaultValue("/Image/Profile/default.jpg");
        }
    }
}
