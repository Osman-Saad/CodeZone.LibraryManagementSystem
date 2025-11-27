using LibraryManagementSystem.BLL.IServices;
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
    public class BorrowTransactionService : IBorrowTransactionService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IBookService _bookService;

        public BorrowTransactionService(LibraryDbContext dbContext,IBookService bookService)
        {
            this._dbContext = dbContext;
            this._bookService = bookService;
        }
        public async Task BorrowAsync(Guid bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId)??throw new Exception("Book Not Found");
            var transaction = new BorrowTransaction
            {
                BookId = bookId,
                BorrowedDate = DateTime.UtcNow,
            };
            await _dbContext.BorrowTransactions.AddAsync(transaction);
            book.IsBorrowed = true;
            _bookService.UpdateAsync(book);
            var result = await _dbContext.SaveChangesAsync();
            if(result<=0)
            {
                throw new Exception("Failed to borrow the book.");
            }
        }

      
        public async Task<IEnumerable<Book>> ListTransactionsAsync(string? status, DateOnly? borrowDate, DateOnly? returnDate)
        {
            IQueryable<Book> query = _dbContext.Books;

            if (status == "borrowed")
                query = query.Where(b => b.IsBorrowed);
            else if (status == "available")
                query = query.Where(b => !b.IsBorrowed);

            if (borrowDate.HasValue || returnDate.HasValue)
            {
                query = query.Where(b => b.Transactions.Any(t =>
                    (borrowDate.HasValue &&
                     t.BorrowedDate >= borrowDate.Value.ToDateTime(TimeOnly.MinValue) &&
                     t.BorrowedDate <= borrowDate.Value.ToDateTime(TimeOnly.MaxValue))
                    ||
                    (returnDate.HasValue &&
                     t.ReturnedDate.HasValue &&
                     t.ReturnedDate.Value >= returnDate.Value.ToDateTime(TimeOnly.MinValue) &&
                     t.ReturnedDate.Value <= returnDate.Value.ToDateTime(TimeOnly.MaxValue))
                ));
            }

            return await query.ToListAsync();
        }

        public async Task ReturnAsync(Guid bookId)
        {
            var transaction = await _dbContext.BorrowTransactions
                .Where(t => t.BookId == bookId && t.ReturnedDate == null)
                .FirstOrDefaultAsync() ?? throw new Exception("No active borrow transaction found for this book.");
            transaction.ReturnedDate = DateTime.UtcNow;
            var book = await _bookService.GetByIdAsync(bookId) ?? throw new Exception("Book Not Found");
            book.IsBorrowed = false;
            _bookService.UpdateAsync(book);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to return the book.");
            }
        }
    }
}
