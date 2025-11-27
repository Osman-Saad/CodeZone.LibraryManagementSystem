using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.PL.Validation;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.PL.Models
{
    public class AuthorViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [UniqueAuthorFullName] 
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Website { get; set; }
        [MaxLength(300)]

        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
