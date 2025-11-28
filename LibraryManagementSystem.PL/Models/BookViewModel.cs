using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.PL.Models
{
    public class BookViewModel
    {
        public Guid? Id { get; set; } 

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public bool IsBorrowed { get; set; } = false;
    }
}
