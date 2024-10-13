using Blog.Data.Entities;
using Blog.Data.Models;

namespace Blog.Data.Interfaces.Application
{
    public interface IPostApplicationServices
    {
        Task<Post> AddAsync(CreatePostModel model, string? userId);
        Task<Post> UpdateAsync(UpdatePostModel model);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(long id);
        Task<Post> AddCommentAsync(long postId, string? userId, CommentPostModel model);
    }
}
