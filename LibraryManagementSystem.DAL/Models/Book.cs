using LibraryManagementSystem.DAL.Enums;

namespace LibraryManagementSystem.DAL.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public ICollection<BorrowTransaction> Transactions { get; set; } = new List<BorrowTransaction>();
    }
}
