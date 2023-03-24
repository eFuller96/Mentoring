using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DatabaseConfig;

public class MovieDbContext : DbContext
{
    public virtual DbSet<MovieModel> Movies { get; set; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
        
    }
}