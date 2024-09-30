using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Entities;

namespace Blog.Data.Contexts.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("blog_comments");
            builder.HasOne(p => p.Author)
                    .WithOne()
                    .HasForeignKey<Comment>(p => p.AuthorId);
            builder.HasMany(p => p.Likes)
                    .WithOne(p => p.Comment)
                    .HasForeignKey(p => p.CommentId);
        }
    }
}
