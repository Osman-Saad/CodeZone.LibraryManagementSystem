

namespace LibraryManagementSystem.DAL.Models
{
    public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Website { get; set; }

        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
