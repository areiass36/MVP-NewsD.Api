using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsD.Model;

namespace NewsD.DataAccess.Configuration;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("News", NewsDDataContext.DBO_SCHEMA)
               .HasKey(n => n.Id)
               .HasName("Id");

        builder.Property(n => n.Title)
               .HasColumnName("Title");

        builder.Property(n => n.Content)
               .HasColumnName("Content");

        builder.Property(n => n.Likes)
               .HasColumnName("Likes");

        builder.Ignore(n => n.Source);

        builder.Ignore(n => n.Topics);

        builder.Property(n => n.CreatorId)
              .HasColumnName("Creator");

        builder.Property(n => n.ImageUrl)
               .HasColumnName("ImageUrl");

        builder.HasOne(n => n.Creator).WithMany(u => u.News).HasForeignKey(n => n.CreatorId);
    }
}