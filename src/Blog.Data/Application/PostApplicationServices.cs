using Blog.Data.Entities;
using Blog.Data.Interfaces.Application;
using Blog.Data.Interfaces.Repositories;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.Data.Application
{
    public class PostApplicationServices : IPostApplicationServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        
        public PostApplicationServices(IPostRepository postRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }

        public async Task<Post> AddAsync(CreatePostModel model, string userId)
        {            
            if (userId == null)
                return null; //TODO validar se author está cadastrado

            var author = await _authorRepository.GetAsNotTrackingAsync(x => x.User.Id == userId);

            var post = Post.Create(model.Title, model.Description, author, model.SubTitle);
            await _postRepository.InsertAsync(post);
            await _postRepository.CommitAsync();    
            return post;
        }

        public async Task<Post> UpdateAsync(UpdatePostModel model)
        {
            var post = await _postRepository.GetAsTrackingAsync(x => x.Id == model.Id);
            post.Change(model.Title, model.Description, model.SubTitle);
            await _postRepository.CommitAsync();
            return post;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _postRepository.DeleteAsync(id);
            if (result)
                await _postRepository.CommitAsync();
            return result;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _postRepository.GetPagedAsNoTrackingAsync(1, 10);
        }

        public async Task<Post> GetPostAsync(long id)
        {
            return await _postRepository.GetAsNotTrackingAsync(x => x.Id == id);
        }

        public async Task<Post> AddCommentAsync(long postId, string userId, CommentPostModel model)
        {
            var post = await _postRepository.GetAsTrackingAsync(x => x.Id == postId);
            var comment = Comment.Create(model.Description, userId, postId);
            post.AddComment(comment);
            await _postRepository.CommitAsync();
            return post;
        }
    }
}
