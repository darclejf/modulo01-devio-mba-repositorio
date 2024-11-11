namespace Blog.Web.Models
{
	public class ListItemPostViewModel
	{
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UrlImage { get; set; }
    }
}
