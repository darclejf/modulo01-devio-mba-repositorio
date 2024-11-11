using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Models
{
    public class CommentPostModel
    {
        public long Id { get; set; }
        public long PostId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}
