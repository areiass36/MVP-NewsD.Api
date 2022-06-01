using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsD.Model;

namespace NewsD.DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("Entity", NewsDDataContext.DBO_SCHEMA)
               .HasKey(u => u.Id)
               .HasName("Id");

        builder.Property(u => u.Name)
               .HasColumnName("Name");

        builder.Property(u => u.ProfilePhotoUrl)
               .HasColumnName("PhotoUrl");

        builder.Property(u => u.Email)
               .HasColumnName("Email");

        builder.Property(u => u.Password)
               .HasColumnName("Password");

        builder.Property(u => u.CreationDate)
               .HasColumnName("CreationDate");

        builder.Property(u => u.LastUpdate)
               .HasColumnName("LastUpdateDate");

        builder.Property(u => u.Role)
               .HasColumnName("Role");

        builder.HasMany(u => u.News).WithOne(n => n.Creator).HasForeignKey(n => n.CreatorId);
    }
}