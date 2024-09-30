namespace Blog.Api.Models
{
    public class PostCreateApiModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
    }
}
