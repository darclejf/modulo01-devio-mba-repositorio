using Blog.Data.Entities;
using Blog.Data.Interfaces.Application;
using Blog.Data.Interfaces.Repositories;
using Microsoft.Extensions.Hosting;

namespace Blog.Data.Application
{
    public class PostApplicationServices : IPostApplicationServices
    {
        private readonly IPostRepository _postRepository;
        public PostApplicationServices(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> AddAsync(Post post)
        {
            await _postRepository.InsertAsync(post);
            await _postRepository.CommitAsync();    
            return post;
        }

        public Task<Post> UpdateAsync(long id, Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _postRepository.DeleteAsync(id);
            if (result)
                await _postRepository.CommitAsync();
            return result;
        }

        public async Task<Post> UpdateAsync(long id, string title, string description)
        {
            var post = await _postRepository.GetAsTrackingAsync(p => p.Id == id);
            post.ChangeTitle(title);
            post.ChangeDescription(description);            
            await _postRepository.CommitAsync();
            return post;
        }
    }
}
