namespace Blog.Data.Entities
{
    public class Post : AbstractEntity
    {
        public string Title { get; protected set; }
        public string SubTitle { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public bool Active { get; protected set; }
        //public int NumberOfViews { get; protected set; }
        public Author Author { get; protected set; }
        public long AuthorId { get; protected set; }

        private readonly IList<Comment> _comments = [];
        public IEnumerable<Comment> Comments { get { return _comments; } }

        protected Post() { }

        public static Post Create(string title, string description, Author author, string subTitle = "")
        {
            //TODO validacoes
            return new Post { 
                Title = title, 
                Description = description, 
                CreatedAt = DateTime.Now, 
                UpdateAt = DateTime.Now,
                Active = true, 
                AuthorId = author.Id,
                SubTitle = subTitle
            };   
        }

        public void Activate() 
        { 
            Active = true;
            UpdateAt = DateTime.Now;
        }

        public void Deactivate() 
        { 
            Active = false;
            UpdateAt = DateTime.Now;
        }
        
        public void Change(string title, string description, string subTitle) 
        {
            //TODO validacoes
            Title = title;
            Description = description;
            SubTitle = subTitle;
            UpdateAt = DateTime.Now;
        } 

        public void AddComment(string description, string userId)
        {
            _comments.Add(Comment.Create(description, userId, Id));
        }

        public void DeleteComment(Comment comment)  
        {
            _comments.Remove(comment);
        }
    }
}
