using BookListMVC.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{

    // since we want to use identity we have to inherit from IdentityDbContext
    // public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<User.User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // needed after adding identity since where was no migration allowed for now
            // error with some keys idk
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedBooks();
            modelBuilder.SeedUsers();
        }
    }
}
