using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.IServices
{
    public interface IAuthorService
    {
        Task AddAsync(Author author);
        Task<Author?> GetByIdAsync(Guid id);
        Task<IEnumerable<Author>> GetAllAsync();
        void UpdateAsync(Author author);
        void DeleteAsync(Author author);
        Task<int> CompleteAsync();

    }
}
