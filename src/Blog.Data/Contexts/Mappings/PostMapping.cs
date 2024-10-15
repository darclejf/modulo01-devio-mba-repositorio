using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Entities;

namespace Blog.Data.Contexts.Mappings
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("blog_posts");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(250);

            builder.HasOne(p => p.Author)
                    .WithMany()
                    .HasForeignKey(p => p.AuthorId);

            builder.HasMany(p => p.Comments)
                    .WithOne(p => p.Post)
                    .HasForeignKey(p => p.PostId);
        }
    }
}
