﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class CreatePostModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Título")]
        public string Title { get; set; }
        
        [Display(Name = "SubTítulo")]
        public string SubTitle { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}
