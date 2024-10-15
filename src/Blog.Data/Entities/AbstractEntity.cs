using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities
{
    public abstract class AbstractEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
