namespace Blog.Data.Entities
{
    public class CommentLike(bool isLiked, string userId, long commentId) : AbstractEntity //TODO testando primary constructor
    {
        public bool IsLiked { get; protected set; } = isLiked;
        public string UserId { get; protected set; } = userId;
        public Author User { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public long CommentId { get; protected set; } = commentId;
        public Comment Comment { get; protected set; }

        public void ChangeLike(bool isLiked)
        {
            IsLiked = isLiked;
            CreatedAt = DateTime.Now;
        }
    }
}
