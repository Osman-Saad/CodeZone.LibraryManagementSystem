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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FullName)
                   .IsRequired();
            builder.HasIndex(a=>a.FullName)
                   .IsUnique();

            builder.Property(a => a.Email)
                     .IsRequired();
            builder.HasIndex(a=>a.Email)
                   .IsUnique();

            builder.Property(a => a.Bio)
                .HasMaxLength(300);

            builder.HasMany(a => a.Books)
                   .WithOne(b => b.Author)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
