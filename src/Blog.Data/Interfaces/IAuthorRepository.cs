using Blog.Data.Entities;
using System.Linq.Expressions;

namespace Blog.Data.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> InsertAsync(Author author);
        Task<Author> GetAsNotTrackingAsync(Expression<Func<Author, bool>> spec);
        Task<IEnumerable<Author>> GetAllAsNotTrackingAsync();
        Task<IEnumerable<Author>> SearchAsNotTrackingAsync(string search);
        Author Delete(Author author);
        Task CommitAsync();
    }
}
