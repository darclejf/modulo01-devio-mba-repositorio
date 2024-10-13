using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities
{
    public class CommentLike : AbstractEntity //TODO testando primary constructor
    {
        public bool IsLiked { get; protected set; }
        public string UserId { get; protected set; }
        public IdentityUser User { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public long CommentId { get; protected set; }
        public Comment Comment { get; protected set; }

        protected CommentLike() { }
        public static CommentLike Create(bool isLiked, IdentityUser user, long commentId)
        {
            return new CommentLike()
            {
                IsLiked = isLiked,
                UserId = user.Id,
                CommentId = commentId
            };
        }

        public void ChangeLike(bool isLiked)
        {
            IsLiked = isLiked;
            CreatedAt = DateTime.Now;
        }
    }
}
