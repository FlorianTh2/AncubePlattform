using BookListMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedBooks(this ModelBuilder modelBuilder)
        {

            // if json file filled with data exists, following is also possible
               //
            // string file = System.IO.File.ReadAllText("data.json");
            // var books = JsonSerializer.Deserialize<List<Book>>(file);

            modelBuilder.Entity<Book>().HasData(
                    new Book
                    {
                        Id = 1,
                        Name="The Great Book",
                        Author="Max Mustermann"
                    },
                    new Book
                    {
                        Id = 2,
                        Name="Another Great Book",
                        Author="Max MusterMustermann"
                    }
                );
        }
    }
}