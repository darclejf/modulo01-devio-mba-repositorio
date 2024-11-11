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

        public async Task<IEnumerable<Author>> GetAllAsNotTrackingAsync()
        {
            return await _dbContext.Authors
                                    .Include(x => x.User)   
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Author>> SearchAsNotTrackingAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
                return await GetAllAsNotTrackingAsync();

            return await _dbContext.Authors
                                    .Include(x => x.User)
                                    .Where(x => x.User.UserName.Contains(search) || 
                                                x.Name.Contains(search))
                                    .AsNoTracking()
                                    .ToListAsync();
        }

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

        public Author Delete(Author author)
        {
            var result = _dbContext.Authors.Remove(author);
            return result.Entity;
        }

    }
}
