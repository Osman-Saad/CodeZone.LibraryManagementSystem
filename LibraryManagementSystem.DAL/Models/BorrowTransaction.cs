using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class BorrowTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public Book Book { get; set; } 
        public DateOnly BorrowedDate { get; set; }
        public DateOnly? ReturnedDate { get; set; }
    }
}
