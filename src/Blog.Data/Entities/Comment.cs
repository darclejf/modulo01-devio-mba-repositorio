namespace Blog.Data.Entities
{
    public class Comment : AbstractEntity
    {
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }
        public int NumberOfLikes { get; protected set; } //TODO vale apena ter um contador de likes ou totalizar atraves do total da lista de likes?
        public int NumberOfDislikes { get; protected set; }
        public ICollection<CommentLike> Likes {  get; protected set; } = [];
        public string AuthorId { get; protected set; }
        public Author Author { get; protected set; } 
        public long PostId { get; protected set; }
        public Post Post { get; protected set; }

        protected Comment() { }

        public static Comment Create(string description, string authorId, long postId)
        {
            return new Comment() { Description = description, CreateAt = DateTime.Now, AuthorId = authorId, PostId = postId }; 
        }

        public void Like(string userId) 
        { 
            NumberOfLikes++;
            var like = Likes.FirstOrDefault(x => x.UserId == userId);
            if (like == null)
                Likes.Add(new CommentLike(true, userId, Id));
            else
            {
                like.ChangeLike(true);
                NumberOfDislikes--;
            }
        }

        public void Dislike(string userId) 
        { 
            NumberOfDislikes++;
            var like = Likes.FirstOrDefault(x => x.UserId == userId);
            if (like == null)
                Likes.Add(new CommentLike(true, userId, Id));
            else
            {
                like.ChangeLike(true);
                NumberOfLikes--;
            }
        }
    }
}
