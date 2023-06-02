using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EF.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
               .HasMaxLength(250);

        builder.Property(e => e.Email)
               .HasMaxLength(100);

        builder.Property(e => e.Password)
               .HasMaxLength(100);

        builder.HasIndex(e => e.Key).IsUnique(false);
        builder.HasIndex(e => e.Email).IsUnique(true);
    }
}
