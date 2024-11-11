namespace Blog.Web.Models
{
    public class CommentPostViewModel
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
