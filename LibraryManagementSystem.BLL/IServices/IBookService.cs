using LibraryManagementSystem.BLL.ProjectionModel;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.IServices
{
    public interface IBookService
    {
        Task AddAsync(Book book);
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();

        void UpdateAsync(Book book);
        void DeleteAsync(Book book);
        Task<IEnumerable<BookSelectItem>> GetForSelectAsync();

        Task<int> CompleteAsync();

    }
}
