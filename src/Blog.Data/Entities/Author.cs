using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities
{
    public class Author : AbstractEntity //IdentityUser TODO como faco para extender p identytuser e criar o registro com disciminator Author? 
    {
        public string UserId { get; private set; }
        public IdentityUser User { get; private set; }
        public string Name { get; private set; }

        public static Author Create(string name, IdentityUser user)
        {
            return new Author
            {
                Name = name,
                User = user,
                UserId = user.Id
            };
        }
    }
}
