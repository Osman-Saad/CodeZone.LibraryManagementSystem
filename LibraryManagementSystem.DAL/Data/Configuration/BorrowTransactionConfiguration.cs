using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Data.Configuration
{
    public class BorrowTransactionConfiguration : IEntityTypeConfiguration<BorrowTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowTransaction> builder)
        {
            builder.HasKey(bt => bt.Id);

            builder.Property(bt => bt.BorrowedDate)
                   .IsRequired();
        }
    }
}
