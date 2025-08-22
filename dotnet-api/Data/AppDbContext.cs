using DotnetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.Data
{
    /// <summary>
    /// Entity Framework Core database context. This class defines the shape of
    /// the database by declaring DbSet properties for each entity. It is
    /// configured in Program.cs to use a MySQL provider.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Table of items.
        /// </summary>
        public DbSet<Item> Items => Set<Item>();
    }
}