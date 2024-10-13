using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities
{
    public class Comment : AbstractEntity
    {
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }
        public int NumberOfLikes { get; protected set; } //TODO vale apena ter um contador de likes ou totalizar atraves do total da lista de likes?
        public int NumberOfDislikes { get; protected set; }
        public ICollection<CommentLike> Likes {  get; protected set; } = [];
        public IdentityUser User { get; protected set; }
        public string UserId { get; protected set; }
        public long PostId { get; protected set; }
        public Post Post { get; protected set; }
        

        protected Comment() { }

        public static Comment Create(string? description, string userId, long postId)
        {
            return new Comment() { 
                Description = description, 
                CreateAt = DateTime.Now, 
                UserId = userId,
                PostId = postId 
            }; 
        }

        public void Like(IdentityUser user) 
        { 
            NumberOfLikes++;
            var like = Likes.FirstOrDefault(x => x.User.Id == user.Id);
            if (like == null)
                Likes.Add(CommentLike.Create(true, user, Id));
            else
            {
                like.ChangeLike(true);
                NumberOfDislikes--;
            }
        }

        public void Dislike(IdentityUser user) 
        { 
            NumberOfDislikes++;
            var like = Likes.FirstOrDefault(x => x.User.Id == user.Id);
            if (like == null)
                Likes.Add(CommentLike.Create(true, user, Id));
            else
            {
                like.ChangeLike(true);
                NumberOfLikes--;
            }
        }
    }
}
