using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Data.Seed
{
    public static class SeedContext
    {

        public static async Task SeedAsync(LibraryDbContext dbContext)
        {
            

            if (!dbContext.Authors.Any())
            {
                var authorsJson = File.ReadAllText("../LibraryManagementSystem.DAL/Data/Seed/authors.json");
                var authors = JsonSerializer.Deserialize<List<Author>>(authorsJson);
                await dbContext.AddRangeAsync(authors);
            }

            if (!dbContext.Books.Any())
            {
                var booksJson = File.ReadAllText("../LibraryManagementSystem.DAL/Data/Seed/books.json");
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                var books = JsonSerializer.Deserialize<List<Book>>(booksJson, options);
                await dbContext.AddRangeAsync(books);
            }
            if (!dbContext.BorrowTransactions.Any())
            {
                var transactionsJson = File.ReadAllText("../LibraryManagementSystem.DAL/Data/Seed/transactions.json");
                var transactions = JsonSerializer.Deserialize<List<BorrowTransaction>>(transactionsJson);
                await dbContext.AddRangeAsync(transactions);
            }
            
            await dbContext.SaveChangesAsync();
        }
    }
}
