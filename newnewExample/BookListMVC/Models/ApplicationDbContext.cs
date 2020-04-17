using BookListMVC.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<User.User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedBooks();
            modelBuilder.SeedUsers();
        }
    }
}
