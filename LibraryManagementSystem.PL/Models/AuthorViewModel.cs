using LibraryManagementSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.PL.Models
{
    public class AuthorViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [RegularExpression(@"^(\w{2,}\s){3}\w{2,}$", ErrorMessage = "Full name must contain four names, each at least 2 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        public string? Website { get; set; }
        [MaxLength(300, ErrorMessage = "Maxlength is 300 characters.")]
        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
