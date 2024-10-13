using Blog.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class CommentPostModel
    {
        public long Id { get; set; }
        public long PostId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        public CommentPostModel() { }
        public CommentPostModel(Comment entity)
        {
            Id = entity.Id;
            PostId = entity.PostId;
            Description = entity.Description;
        }
    }
}
