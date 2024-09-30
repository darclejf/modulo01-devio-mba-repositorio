using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities
{
    public class Author : IdentityUser
    {
        public string Name { get; private set; }

        protected Author() { }

        public static Author Create(string name, string email)
        {
            return new Author
            {
                Name = name,
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };
        }
    }
}
