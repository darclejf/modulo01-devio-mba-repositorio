using Blog.Application.Models;
using Blog.Data.Entities;

namespace Blog.Application.Interfaces
{
	public interface IPostApplicationServices
    {
        Task<Post> AddAsync(CreatePostModel model, string userId);
        Task<Post> UpdateAsync(EditPostModel model, string userId);
        Task<bool> DeleteAsync(long id, string userId);
        Task<IEnumerable<Post>> GetPostsAsync(int page, int total = 10);
        Task<Post> GetPostAsync(long id);
        Task<Post> AddCommentAsync(long postId, string userId, string description);
        Task<bool> DeleteCommentAsync(long id, string userId);
        Task<Comment> GetCommentAsync(long id);
    }
}
