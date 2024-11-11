namespace Blog.Api.Models
{
    public class PostApiModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentApiModel> Comments { get; set; } = [];
    }
}
