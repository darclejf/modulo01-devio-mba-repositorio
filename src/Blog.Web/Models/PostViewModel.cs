using Blog.Application.Models;

namespace Blog.Web.Models
{
	public class PostViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentPostViewModel> Comments { get; set; } = [];
        public string UrlImage { get; set; }
    }
}
