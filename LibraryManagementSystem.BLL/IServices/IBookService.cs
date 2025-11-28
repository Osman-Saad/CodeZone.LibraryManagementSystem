using LibraryManagementSystem.BLL.ProjectionModel;
using LibraryManagementSystem.DAL.Models;

namespace LibraryManagementSystem.BLL.IServices
{
    public interface IBookService
    {
        Task AddAsync(Book book);
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync(int pageNumber);
        Task<IEnumerable<Book>> GetAllAsync();

        void UpdateAsync(Book book);
        void DeleteAsync(Book book);
        Task<IEnumerable<BookSelectItem>> GetForSelectAsync();
        Task<int> GetBookCount();

        Task<int> CompleteAsync();

    }
}
