using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Contexts.Mappings
{
    internal class AuthorMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("blog_authors");
            //builder.HasOne(p => p.User)
            //        .WithOne()
            //        .HasForeignKey("UserId");
        }
    }
}
