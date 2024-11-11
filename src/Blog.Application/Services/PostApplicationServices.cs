using Blog.Data.Entities;
using Blog.Application.Interfaces;
using Blog.Data.Interfaces;
using Blog.Application.Models;
using Blog.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using Blog.Data.Contexts;

namespace Blog.Application.Services
{
    public class PostApplicationServices : IPostApplicationServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public PostApplicationServices(IPostRepository postRepository, IAuthorRepository authorRepository, UserManager<IdentityUser> userManager)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _userManager = userManager;
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

        public async Task<Post> UpdateAsync(EditPostModel model, string userId)
        {
            var post = await _postRepository.GetAsTrackingAsync(x => x.Id == model.Id);
            if (!await ValidateUserAsync(post, userId))
                throw new InvalidUserException();

            post.Change(model.Title, model.Description, model.SubTitle);
            await _postRepository.CommitAsync();
            return post;
        }

        public async Task<bool> DeleteAsync(long id, string userId)
        {
			var post = await _postRepository.GetAsTrackingAsync(x => x.Id == id);
            if (!await ValidateUserAsync(post, userId))
                throw new InvalidUserException();

			var result = await _postRepository.DeleteAsync(id);
            if (result)
                await _postRepository.CommitAsync();
            return result;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int page, int total = 10)
        {
            return await _postRepository.GetPagedAsNoTrackingAsync(page, total);
        }

        public async Task<Post> GetPostAsync(long id)
        {
            return await _postRepository.GetAsNotTrackingAsync(x => x.Id == id);
        }

        public async Task<Post> AddCommentAsync(long postId, string userId, string description)
        {
            var post = await _postRepository.GetAsTrackingAsync(x => x.Id == postId);
            post.AddComment(description, userId);
            await _postRepository.CommitAsync();
            return post;
        }

        public async Task<bool> DeleteCommentAsync(long id, string userId)
        {
            var post = await _postRepository.GetAsTrackingAsync(x => x.Comments.Any(y => y.Id == id));
            var comment = post.Comments.FirstOrDefault(x => x.Id == id);
            if (!await ValidateCommentUserAsync(comment, userId))
                throw new InvalidUserException();

            post.DeleteComment(comment);
            await _postRepository.CommitAsync();
            return true;
        }

        private async Task<bool> ValidateUserAsync(Post post, string userId)
        {
            if (post.Author.UserId == userId)
                return true;

            var author = await _authorRepository.GetAsNotTrackingAsync(x => x.User.Id == userId);
            var roles = await _userManager.GetRolesAsync(author.User);
            return roles.Contains(BlogConstants.ADMINROLE);
        }

        private async Task<bool> ValidateCommentUserAsync(Comment comment, string userId)
        {
            if (comment.UserId == userId)
                return true;

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(BlogConstants.ADMINROLE);
        }

        public async Task<Comment> GetCommentAsync(long id)
        {
            var post = await _postRepository.GetAsNotTrackingAsync(x => x.Comments.Any(y => y.Id == id));
            return post.Comments.FirstOrDefault(x => x.Id == id);
        }
    }
}
