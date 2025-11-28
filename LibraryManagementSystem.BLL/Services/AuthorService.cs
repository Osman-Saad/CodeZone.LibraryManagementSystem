using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.BLL.ProjectionModel;
using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
   
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorService(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(Author author)=>
           await _dbContext.Authors.AddAsync(author);

       
        public async Task<IEnumerable<Author>> GetAllAsync(int pageNumber)=>
            await _dbContext.Authors
            .AsNoTracking()
            .Skip((pageNumber - 1)*5)
            .Take(5)
            .Include(a=>a.Books)
            .ToListAsync();

        public async Task<Author?> GetByIdAsync(Guid id) =>
            await _dbContext.Authors.FindAsync(id);


        void IAuthorService.DeleteAsync(Author author)=>
            _dbContext.Authors.Remove(author);

        void IAuthorService.UpdateAsync(Author author)=>
            _dbContext.Authors.Update(author);

        public async Task<int> CompleteAsync()=>
            await _dbContext.SaveChangesAsync();

        public async Task<bool> EmailIsExist(Guid? id ,string email)=>
            await _dbContext.Authors.AnyAsync(a => a.Email == email && a.Id != id);

        public async Task<bool> FullNameIsExist(Guid? id, string fullName)=>
           await _dbContext.Authors.AnyAsync(a => a.FullName == fullName && a.Id!=id);

        public async Task<IEnumerable<AuthorSelectItem>> GetForSelectAsync() =>
            await _dbContext.Authors
            .AsNoTracking()
            .OrderBy(a => a.FullName)
            .Select(a => new AuthorSelectItem
            {
                Id = a.Id,
                FullName = a.FullName
            }).ToListAsync();

        public async Task<int> GetAuthorCount() =>
           await _dbContext.Authors.CountAsync();
    }
}
