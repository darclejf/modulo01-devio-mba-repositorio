using Blog.Data.Entities;
using System.Linq.Expressions;

namespace Blog.Data.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> InsertAsync(Author author);
        Task<Author> GetAsNotTrackingAsync(Expression<Func<Author, bool>> spec);
        Task CommitAsync();
    }
}
