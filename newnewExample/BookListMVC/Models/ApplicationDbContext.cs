﻿using BookListMVC.Extensions;
using BookListMVC.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    // since we want to use entityFramework and its automaticly mapping to the database
    // we have to device from DbContext-Class
    // public class ApplicationDbContext : DbContext


    // since we want to use identity we have to inherit from IdentityDbContext
    // public class ApplicationDbContext : IdentityDbContext

    // IdenetityDbContext builds up the database with the entityframework-connection
    //      -> per default it uses the IdentityUser, if you want to change, tell
    //         the other user (which has to derive from IdenetityUser) in <>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
