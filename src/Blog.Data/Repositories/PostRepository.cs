using Blog.Data.Contexts;
using Blog.Data.Entities;
using Blog.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
                return false;
            
            _dbContext.Remove(post);
            return true;
        }

        public async Task<Post> GetAsNotTrackingAsync(Expression<Func<Post, bool>> spec)
        {
            var post = await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(spec);
            return post;
        }

        public async Task<Post> GetAsTrackingAsync(Expression<Func<Post, bool>> spec)
        {
            var post = await _dbContext.Posts.AsTracking().FirstOrDefaultAsync(spec);
            return post;
        }

        public Task<IEnumerable<Post>> GetPagedAsNoTrackingAsync(int page, int take)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPagedAsNoTrackingAsync(int page, int take, Expression<Func<Post, bool>> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> InsertAsync(Post post)
        {
            var result = await _dbContext.Posts.AddAsync(post);
            return result.Entity;
        }
    }
}
