using Blog.Data.Entities;

namespace Blog.Data.Interfaces.Application
{
    public interface IPostApplicationServices
    {
        Task<Post> AddAsync(Post post);
        Task<Post> UpdateAsync(long id, string title, string description);
        Task<bool> DeleteAsync(long id);
    }
}
