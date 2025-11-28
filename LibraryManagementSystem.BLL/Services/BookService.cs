using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.BLL.ProjectionModel;
using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(Book book) =>
            await _dbContext.Books.AddAsync(book);

        public async Task<int> CompleteAsync() =>
            await _dbContext.SaveChangesAsync();

        public void DeleteAsync(Book book) =>
            _dbContext.Books.Remove(book);

        public async Task<IEnumerable<Book>> GetAllAsync(int pageNumber) =>
           await _dbContext.Books
            .AsNoTracking()
            .Skip((pageNumber - 1) * 5)
            .Take(5)
            .Include(b => b.Author)
            .ToListAsync();

        public async Task<IEnumerable<Book>> GetAllAsync() =>
             await _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .ToListAsync();


        public async Task<int> GetBookCount() =>
           await _dbContext.Books.CountAsync();


        public async Task<Book?> GetByIdAsync(Guid id) =>
            await _dbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<IEnumerable<BookSelectItem>> GetForSelectAsync() =>
            await _dbContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .Select(b => new BookSelectItem
            {
                Id = b.Id,
                Title = b.Title,
            }).ToListAsync();


        public void UpdateAsync(Book book) =>
              _dbContext.Books.Update(book);
    }
}
