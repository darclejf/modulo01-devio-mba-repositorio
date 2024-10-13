using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class UpdatePostModel
    {
        public long Id { get; set; }    
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
