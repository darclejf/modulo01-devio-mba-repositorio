namespace Blog.Data.Models
{
    public class PostFilterModel
    {
        public string SearchValue { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }
    }
}
