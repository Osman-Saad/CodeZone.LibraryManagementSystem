using LibraryManagementSystem.DAL.Models;

namespace LibraryManagementSystem.BLL.IServices
{
    public interface IBorrowTransactionService
    {
        Task<IEnumerable<Book>> ListTransactionsAsync(string? status, DateOnly? borrowDate, DateOnly? returnDate);
        Task BorrowAsync(Guid bookId);
        Task ReturnAsync(Guid bookId);
    }
}
