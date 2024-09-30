namespace Blog.Api.Models
{
    public class PostApiModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
    }
}
