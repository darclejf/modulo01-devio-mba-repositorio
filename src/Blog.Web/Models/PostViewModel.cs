using Blog.Data.Entities;
using Blog.Data.Models;

namespace Blog.Web.Models
{
    public class PostViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreateAt { get; set; }
        public List<CommentPostModel> Comments { get; set; } = [];
        public string UrlImage { get; set; }

        public PostViewModel() { }

        public PostViewModel(Post entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            SubTitle = entity.SubTitle;
            Description = entity.Description;
            CreateAt = entity.CreatedAt;
            Author = string.IsNullOrEmpty(entity.Author?.Name) ? "" : entity.Author?.Name;
            Comments = entity.Comments == null ? [] : entity.Comments.Select(x => new CommentPostModel(x)).ToList();
        }
    }
}
