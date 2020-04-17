using BookListMVC.Models;
using BookListMVC.Models.User;
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

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Tom H",
                        Email = "asd@asdf.de",
                        Department = Dept.HR
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Tom 2",
                        Email = "asd2@asdf.de",
                        Department = Dept.IT
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Tom 3",
                        Email = "asd3@asdf.de",
                        Department = Dept.HR
                    }
                );
        }
    }
}