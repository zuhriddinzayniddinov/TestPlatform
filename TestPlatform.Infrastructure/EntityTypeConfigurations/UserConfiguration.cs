using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Infrastructure.EntityTypeConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(u => u.Email)
            .IsUnique(true);

        builder.Property(u => u.Email)
            .HasMaxLength(30);
    }
}