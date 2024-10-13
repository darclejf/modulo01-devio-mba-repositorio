using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Entities;

namespace Blog.Data.Contexts.Mappings
{
    public class CommentLikeMapping : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.ToTable("blog_comments_likes");
            builder.HasOne(p => p.Comment)
                    .WithOne()
                    .HasForeignKey<CommentLike>(p => p.CommentId);
        }
    }
}
