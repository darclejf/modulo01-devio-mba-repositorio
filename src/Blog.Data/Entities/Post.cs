namespace Blog.Data.Entities
{
    public class Post : AbstractEntity
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public bool Active { get; protected set; }
        //public int NumberOfViews { get; protected set; }
        public string AuthorId { get; protected set; }
        public Author Author { get; protected set; }

        private readonly IList<Comment> _comments = [];
        public IEnumerable<Comment> Comments { get { return _comments; } }

        protected Post() { }

        public static Post Create(string title, string description, string authorId)
        {
            //TODO validacoes
            return new Post { 
                Title = title, 
                Description = description, 
                CreatedAt = DateTime.Now, 
                UpdateAt = DateTime.Now,
                Active = true, 
                AuthorId = authorId 
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
        
        public void ChangeTitle(string title) 
        {
            //TODO validacoes
            Title = title;
            UpdateAt = DateTime.Now;
        } 

        public void ChangeDescription(string description) 
        {
            Description = description;
            UpdateAt = DateTime.Now;
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }
    }
}
