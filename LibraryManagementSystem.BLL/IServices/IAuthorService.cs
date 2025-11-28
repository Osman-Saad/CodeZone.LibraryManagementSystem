using LibraryManagementSystem.BLL.ProjectionModel;
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
        Task<IEnumerable<Author>> GetAllAsync(int pageNumber);
        void UpdateAsync(Author author);
        void DeleteAsync(Author author);
        Task<IEnumerable<AuthorSelectItem>> GetForSelectAsync();
        Task<bool> EmailIsExist(Guid? id, string email);
        Task<bool> FullNameIsExist(Guid? id, string fullName);
        Task<int> GetAuthorCount();
        Task<int> CompleteAsync();

    }
}
