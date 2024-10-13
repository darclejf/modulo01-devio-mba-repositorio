using Blog.Data.Entities;
using System.Linq.Expressions;

namespace Blog.Data.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Post> InsertAsync(Post post);
        Task<Post?> GetAsTrackingAsync(Expression<Func<Post, bool>> spec);
        Task<Post> GetAsNotTrackingAsync(Expression<Func<Post, bool>> spec);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Post>> GetPagedAsNoTrackingAsync(int page, int take);
        Task<IEnumerable<Post>> GetPagedAsNoTrackingAsync(int page, int take, Expression<Func<Post, bool>> spec);
        Task CommitAsync();
    }
}
