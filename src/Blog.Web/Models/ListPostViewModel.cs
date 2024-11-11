namespace Blog.Web.Models
{
    public class ListPostViewModel
    {
        public int Page { get; set; }
        public IEnumerable<ListItemPostViewModel> Posts { get; set; }
    }
}
