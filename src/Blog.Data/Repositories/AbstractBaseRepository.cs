using Blog.Data.Contexts;

namespace Blog.Data.Repositories
{
    public abstract class AbstractBaseRepository
    {
        protected readonly BlogDbContext _dbContext;

        protected AbstractBaseRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
