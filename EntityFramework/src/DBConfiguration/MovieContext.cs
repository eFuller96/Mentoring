using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.src.DBConfiguration
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions options) : base(options) { }

        public DbSet<MovieModel> Movies { get; set; }
    }
}
