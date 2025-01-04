using Microsoft.EntityFrameworkCore;
using ApiNET.Models;

namespace ApiNET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
    }
}
