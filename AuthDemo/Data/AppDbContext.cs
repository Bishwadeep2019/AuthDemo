using AuthDemo.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
