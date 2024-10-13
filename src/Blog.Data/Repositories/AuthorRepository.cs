using Blog.Data.Contexts;
using Blog.Data.Entities;
using Blog.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace Blog.Data.Repositories
{
    public class AuthorRepository : AbstractBaseRepository, IAuthorRepository
    {
        public AuthorRepository(BlogDbContext dbContext) : base(dbContext) { }

        public async Task<Author> GetAsNotTrackingAsync(Expression<Func<Author, bool>> spec)
        {
            var author = await _dbContext.Authors
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(spec);
            return author;
        }

        public async Task<Author> InsertAsync(Author author)
        {
            var result = await _dbContext.Authors.AddAsync(author);
            return result.Entity;
        }
    }
}
