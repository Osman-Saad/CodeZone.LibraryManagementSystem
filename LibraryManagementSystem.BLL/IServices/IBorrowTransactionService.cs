using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.IServices
{
    public interface IBorrowTransactionService
    {
        Task<IEnumerable<Book>> ListTransactionsAsync(string? status, DateOnly? borrowDate, DateOnly? returnDate);
        Task BorrowAsync(Guid bookId);
        Task ReturnAsync(Guid bookId);
    }
}
