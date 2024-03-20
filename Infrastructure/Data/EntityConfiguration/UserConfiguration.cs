using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id).IsUnique();
        builder.Property(user => user.Id).HasColumnName("id");
        builder.HasIndex(user => user.Login).IsUnique();
        builder.Property(user => user.Login).HasColumnName("login").HasMaxLength(50);
        builder.Property(user => user.Password).HasColumnName("password");
        builder.Property(user => user.Email).HasColumnName("email");
    }
}
