using LibraryManagementSystem.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
