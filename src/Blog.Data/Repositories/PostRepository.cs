using Blog.Data.Contexts;
using Blog.Data.Entities;
using Blog.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Repositories
{
    public class PostRepository : AbstractBaseRepository, IPostRepository
    {
        public PostRepository(BlogDbContext dbContext) : base(dbContext) { }

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
            var post = await _dbContext.Posts
                                        .Include(x => x.Author)
                                        .Include(x => x.Comments)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(spec);
            return post;
        }

        public async Task<Post?> GetAsTrackingAsync(Expression<Func<Post, bool>> spec)
        {
            var post = await _dbContext.Posts
                                        .Include(x => x.Author)
                                        .Include(x => x.Comments)
                                        .AsTracking()
                                        .FirstOrDefaultAsync(spec);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPagedAsNoTrackingAsync(int page, int take)
        {
            var skip = (page-1) * take;
            return await _dbContext.Posts
                                    .Include(post => post.Author)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Skip(skip).Take(take).ToListAsync();
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
