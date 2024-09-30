namespace Blog.Data.DTOs
{
    public class PostFilterDTO
    {
        public string SearchValue { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }
    }
}
