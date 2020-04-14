using AncubePlattformApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AncubePlattformApp.API.Data
{
    // DAO in Java
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        // Name of Prop == Name of Table
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
    }
}