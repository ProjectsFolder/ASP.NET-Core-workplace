using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("tokens");
        builder.HasKey(token => token.Id);
        builder.HasIndex(token => token.Id).IsUnique();
        builder.Property(token => token.Id).HasColumnName("id");
        builder.Property(token => token.Value).HasColumnName("value");
        builder.Property(token => token.CreatedAt).HasColumnName("created_at");
        builder.Property(token => token.UserId).HasColumnName("user_id");
        builder.HasOne(token => token.User).WithMany().HasForeignKey(token => token.UserId);
    }
}
