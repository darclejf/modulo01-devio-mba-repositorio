using Blog.Data.Contexts;
using Blog.Data.Entities;
using Blog.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Repositories
{
	public class AuthorRepository : AbstractBaseRepository, IAuthorRepository
    {
        public AuthorRepository(BlogDbContext dbContext) : base(dbContext) { }

        public async Task<Author> GetAsNotTrackingAsync(Expression<Func<Author, bool>> spec)
        {
            var author = await _dbContext.Authors
                                        .Include(x => x.User)
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
